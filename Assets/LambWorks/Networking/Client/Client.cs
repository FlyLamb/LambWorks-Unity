﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
namespace LambWorks.Networking.Client {
    public partial class Client : MonoBehaviour {
        public static Client instance;
        public static int dataBufferSize = 4096;

        public string ip = "127.0.0.1";
        public int port = 26950;
        public int timeout = 3;
        public int myId = 0;
        public TCP tcp;
        public UDP udp;

        private bool isConnected = false;
        private delegate void PacketHandler(Packet packet);
        private static Dictionary<int, PacketHandler> packetHandlers;

        private void Awake() {
            if (instance == null) {
                instance = this;
            } else if (instance != this) {
                Debug.Log("Instance already exists, destroying object!");
                Destroy(this);
            }
        }

        private void OnApplicationQuit() {
            Disconnect(); // Disconnect when the game is closed
        }

        /// <summary>Attempts to connect to the server.</summary>
        public void ConnectToServer() {
            tcp = new TCP();
            udp = new UDP();

            InitializeClientData();

            isConnected = true;
            tcp.Connect(); // Connect tcp, udp gets connected once tcp is done
        }

        public class TCP {
            public TcpClient socket;

            private NetworkStream stream;
            private Packet receivedData;
            private byte[] receiveBuffer;

            /// <summary>Attempts to connect to the server via TCP.</summary>
            public void Connect() {
                socket = new TcpClient {
                    ReceiveBufferSize = dataBufferSize,
                    SendBufferSize = dataBufferSize
                };

                receiveBuffer = new byte[dataBufferSize];
                
                var ar = socket.BeginConnect(instance.ip, instance.port, ConnectCallback, socket);
                System.Threading.WaitHandle wh = ar.AsyncWaitHandle;
                try {
                    if (!ar.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(2), false)) {
                        socket.Close(); //We have timed out!
                        UnityEngine.SceneManagement.SceneManager.LoadScene(0); //this line is here so that we go back to the menu
                        return;
                    }             
                }
                finally {
                    wh.Close();
                }
            }

            /// <summary>Initializes the newly connected client's TCP-related info.</summary>
            private void ConnectCallback(IAsyncResult result) {
                socket.EndConnect(result);

                if (!socket.Connected) {
                    Client.instance.Disconnect();
                    return;
                }

                stream = socket.GetStream();

                receivedData = new Packet();

                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
            }

            /// <summary>Sends data to the client via TCP.</summary>
            /// <param name="packet">The packet to send.</param>
            public void SendData(Packet packet) {
                try {
                    if (socket != null) {
                        stream.BeginWrite(packet.ToArray(), 0, packet.Length(), null, null); // Send data to server
                    }
                }
                catch (Exception ex) {
                    Debug.Log($"Error sending data to server via TCP: {ex}");
                }
            }

            /// <summary>Reads incoming data from the stream.</summary>
            private void ReceiveCallback(IAsyncResult result) {
                try {
                    int byteLength = stream.EndRead(result);
                    if (byteLength <= 0) {
                        instance.Disconnect();
                        return;
                    }

                    byte[] data = new byte[byteLength];
                    Array.Copy(receiveBuffer, data, byteLength);

                    receivedData.Reset(HandleData(data)); // Reset receivedData if all data was handled
                    stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
                }
                catch {
                    Disconnect();
                }
            }

            /// <summary>Prepares received data to be used by the appropriate packet handler methods.</summary>
            /// <param name="data">The recieved data.</param>
            private bool HandleData(byte[] data) {
                int packetLength = 0;

                receivedData.SetBytes(data);

                if (receivedData.UnreadLength() >= 4) {
                    // If client's received data contains a packet
                    packetLength = receivedData.ReadInt();
                    if (packetLength <= 0) {
                        // If packet contains no data
                        return true; // Reset receivedData instance to allow it to be reused
                    }
                }

                while (packetLength > 0 && packetLength <= receivedData.UnreadLength()) {
                    // While packet contains data AND packet data length doesn't exceed the length of the packet we're reading
                    byte[] packetBytes = receivedData.ReadBytes(packetLength);
                    ThreadManager.ExecuteOnMainThread(() => {
                        using (Packet packet = new Packet(packetBytes)) {
                            int packetId = packet.ReadInt();
                            packetHandlers[packetId](packet); // Call appropriate method to handle the packet
                        }
                    });

                    packetLength = 0; // Reset packet length
                    if (receivedData.UnreadLength() >= 4) {
                        // If client's received data contains another packet
                        packetLength = receivedData.ReadInt();
                        if (packetLength <= 0) {
                            // If packet contains no data
                            return true; // Reset receivedData instance to allow it to be reused
                        }
                    }
                }

                if (packetLength <= 1) {
                    return true; // Reset receivedData instance to allow it to be reused
                }

                return false;
            }

            /// <summary>Disconnects from the server and cleans up the TCP connection.</summary>
            private void Disconnect() {
                instance.Disconnect();

                stream = null;
                receivedData = null;
                receiveBuffer = null;
                socket = null;
            }
        }

        public class UDP {
            public UdpClient socket;
            public IPEndPoint endPoint;

            public UDP() {
                endPoint = new IPEndPoint(IPAddress.Parse(instance.ip), instance.port);
            }

            /// <summary>Attempts to connect to the server via UDP.</summary>
            /// <param name="localPort">The port number to bind the UDP socket to.</param>
            public void Connect(int localPort) {
                socket = new UdpClient(localPort);

                socket.Connect(endPoint);
                socket.BeginReceive(ReceiveCallback, null);

                using (Packet packet = new Packet()) {
                    SendData(packet);
                }
            }

            /// <summary>Sends data to the client via UDP.</summary>
            /// <param name="packet">The packet to send.</param>
            public void SendData(Packet packet) {
                try {
                    packet.InsertInt(instance.myId); // Insert the client's ID at the start of the packet
                    if (socket != null) {
                        socket.BeginSend(packet.ToArray(), packet.Length(), null, null);
                    }
                }
                catch (Exception ex) {
                    Debug.Log($"Error sending data to server via UDP: {ex}");
                }
            }

            /// <summary>Receives incoming UDP data.</summary>
            private void ReceiveCallback(IAsyncResult result) {
                try {
                    byte[] data = socket.EndReceive(result, ref endPoint);
                    socket.BeginReceive(ReceiveCallback, null);

                    if (data.Length < 4) {
                        instance.Disconnect();
                        return;
                    }

                    HandleData(data);
                }
                catch {
                    Disconnect();
                }
            }

            /// <summary>Prepares received data to be used by the appropriate packet handler methods.</summary>
            /// <param name="data">The recieved data.</param>
            private void HandleData(byte[] data) {
                using (Packet packet = new Packet(data)) {
                    int packetLength = packet.ReadInt();
                    data = packet.ReadBytes(packetLength);
                }

                ThreadManager.ExecuteOnMainThread(() => {
                    using (Packet packet = new Packet(data)) {
                        int packetId = packet.ReadInt();
                        packetHandlers[packetId](packet); // Call appropriate method to handle the packet
                    }
                });
            }

            /// <summary>Disconnects from the server and cleans up the UDP connection.</summary>
            private void Disconnect() {
                instance.Disconnect();

                endPoint = null;
                socket = null;
            }
        }

        /// <summary>Initializes all necessary client data.</summary>
        private void InitializeClientData() {
            RegisterHandlers();
            Debug.Log("Initialized packets.");
        }

        /// <summary>Disconnects from the server and stops all network traffic.</summary>
        public void Disconnect() {
            if (isConnected) {
                isConnected = false;
                GameManager.players = new Dictionary<int, PlayerManager>();
                foreach (Entity e in GameManager.entities.Values) Destroy(e.gameObject);

                GameManager.entities = new Dictionary<uint, Entity>();
                tcp.socket.Close();
                if(udp.socket != null)
                udp.socket.Close();

                Debug.Log("Disconnected from server.");
            }
        }
    }
}