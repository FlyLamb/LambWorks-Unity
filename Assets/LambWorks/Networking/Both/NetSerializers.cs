using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;

namespace LambWorks.Networking {


    public class NetSerializers {
        public static Dictionary<int, Func<Packet, object>> deserializers;
        public static Dictionary<Type, int> serializers;

        public static void FindSerializers() {
            if (deserializers != null) return;


            deserializers = new Dictionary<int, Func<Packet, object>>();
            serializers = new Dictionary<Type, int>();

            //deserializers.Add("", (w) => (new TestClass() as INetSerializable).Deserialize(w));

            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(w => Regex.Matches(w.FullName, "System|Unity|mscore|Mono").Count == 0);
            foreach (var assembly in assemblies) {
                RegisterFromAssembly(assembly);
            }
        }

        private static void RegisterFromAssembly(Assembly assembly) { // reflection hell
            var types = assembly.GetTypes().Where(t => t.GetCustomAttributes<NetSerializeAsAttribute>().Count() != 0);
            foreach (var v in types) {
                var attr = v.GetCustomAttribute<NetSerializeAsAttribute>();
                int serializeAs = Hash(attr.serializeAs);
                Type t = v;
                Func<Packet, object> func = (arr) => v.GetMethod("Deserialize").Invoke(v.GetConstructor(Type.EmptyTypes).Invoke(null), new object[] { arr }); // wtf

                deserializers.Add(serializeAs, func);
                serializers.Add(t, serializeAs);
                Debug.Log($"Found object serializer {Hash(attr.serializeAs)} [{attr.serializeAs}] for type {v.Name}");
            }
        }

        public static int Hash(string s) {
            var mystring = s;
            MD5 md5Hasher = MD5.Create();
            var hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(mystring));
            int w = BitConverter.ToInt32(hashed, 0);
            if (w == int.MaxValue || w == int.MinValue) return Hash(s + "-");
            return w;
        }
    }
}