using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using System.Text.RegularExpressions;

namespace LambWorks.Networking {


    public class NetSerializers {
        public static Dictionary<string, Func<Packet, object>> deserializers;
        public static Dictionary<Type, string> serializers;

        public static void FindSerializers() {
            if (deserializers != null) return;


            deserializers = new Dictionary<string, Func<Packet, object>>();
            serializers = new Dictionary<Type, string>();

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
                string serializeAs = attr.serializeAs;
                Type t = v;
                Func<Packet, object> func = (arr) => v.GetMethod("Deserialize").Invoke(v.GetConstructor(Type.EmptyTypes).Invoke(null), new object[] { arr }); // wtf

                deserializers.Add(serializeAs, func);
                serializers.Add(t, serializeAs);
                Debug.Log($"Found serializer {serializeAs}");
            }
        }
    }
}