using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SlimDX;

namespace Engine {
    public enum VariableType {
        Bool, Color, Float, Number, String, Vector, Unknown
    }

    public class Variable {
        private VariableType _type;
        private readonly string _name;
        private object _data;

        private void ParseStream(string name, Stream file) {
            
            if (file == null) {
                return;
            }

            var reader = new StreamReader(file);
            var readLine = reader.ReadLine();

            if (readLine != null) {
                var buffer = Tokenize(readLine);
                
                switch (buffer.First()) {
                    case "bool":
                        _type = VariableType.Bool;
                        bool value;
                        _data = bool.TryParse(buffer[1], out value) && value;
                        break;
                    case "color":
                        _type = VariableType.Color;
                        var color = new Color4 {
                            Red = Convert.ToSingle(buffer[1]),
                            Green = Convert.ToSingle(buffer[2]), 
                            Blue = Convert.ToSingle(buffer[3]), 
                            Alpha = Convert.ToSingle(buffer[4])
                        };
                        _data = color;
                        break;
                    case "float":
                        _type = VariableType.Float;
                        float f;
                        _data = float.TryParse(buffer[1], out f) ? f : 0.0f;
                        break;
                    case "number":
                        _type = VariableType.Number;
                        long l;
                        _data = long.TryParse(buffer[1], out l) ? l : 0;
                        break;
                    case "string":
                        _type = VariableType.String;
                        _data = buffer[1];
                        break;
                    case "vector":
                        _type = VariableType.Vector;
                        float x = float.TryParse(buffer[1], out x) ? x : 0.0f;
                        float y = float.TryParse(buffer[2], out y) ? y : 0.0f;
                        float z = float.TryParse(buffer[3], out z) ? z : 0.0f;
                        var v = new Vector3(
                            x,
                            y,
                            z
                            );
                        _data = v;
                        break;
                    default:
                        _type = VariableType.Unknown;
                        _data = buffer[1];
                        break;
                }
            }
        }

        private static List<string> Tokenize(string readLine) {
            var ret = new List<string>();
            var inQuotes = false;
            var accum = "";
            foreach (var c in readLine) {
                if (c == '\"') {
                    if (inQuotes) {
                        inQuotes = false;
                        ret.Add(accum);
                        accum = "";
                        continue;
                    }
                    inQuotes = true;
                    continue;
                }
                if (c == ' ' && !inQuotes) {
                    ret.Add(accum);
                    accum = "";
                    continue;
                }
                accum += c;
            }
            ret.Add(accum);
            return ret;
        }

        public Variable(string name, VariableType type, object value) {
            _name = name;
            _type = type;
            _data = value;
        }

        public Variable(string buffer) {
            var i = buffer.IndexOf(' ');
            var name = buffer.Substring(0, i);
            var data = buffer.Substring(i);
            ParseStream(name, new MemoryStream(Encoding.ASCII.GetBytes(data)));
        }

        public Variable(string name, Stream file) {
            ParseStream(name, file);
        }

        ~Variable() {
            
        }

        public VariableType Type { get { return _type; } }
        public string Name { get { return _name; } }
        public object Data { get { return _data; } }
    }
}
