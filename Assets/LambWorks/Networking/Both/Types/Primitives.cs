using System;
using System.Collections.Generic;

namespace LambWorks.Networking.Types {
    public enum Primitives {
        integer64,
        integer32,
        integer16,
        integer8,
        boolean,
        floating32,
        text

    }

    public class Primitive {
        private static List<Type> types = new List<Type>() {
            typeof(int), typeof(long), typeof(short), typeof(byte), typeof(bool), typeof(float), typeof(string)
        };

        public static bool IsRecognizedPrimitive(object o) {
            return types.Contains(o.GetType());
        }
    }
}