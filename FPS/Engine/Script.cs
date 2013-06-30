using System;
using System.Collections.Generic;
using System.IO;
using SlimDX;

namespace Engine {
    public class Script : Resource<Script> {
        private readonly Dictionary<string, Variable> _variables; 

        public Script(string name, string path = "./") : base(name, path) {
            _variables = new Dictionary<string, Variable>();
            if (File.Exists(FileName)) {
                using (var f = new StreamReader(FileName)) {
                    string buffer;
                    var read = false;
                    while (!string.IsNullOrEmpty((buffer = f.ReadLine()))) {
                        if (read) {
                            if (buffer.StartsWith("#end")) {
                                read = false;
                            } else {
                                var v = new Variable(buffer);
                                _variables[v.Name] = v;
                            }
                        } else if (buffer.StartsWith("#begin")) {
                            read = true;
                        }
                    }
                }
            }
        }
        ~Script() {
            _variables.Clear();
        }

        public int VariableCount {
            get { return _variables.Count; }
        }

        public void AddVariable(string name, VariableType type, object value) {
            if (!string.IsNullOrEmpty(name) && _variables.ContainsKey(name)) {
                SetVariable(name, value);
            }
            if (name != null) {
                _variables[name]  = new Variable(name, type, value);
            }
        }
        public void SetVariable(string name, object value) {
            if (string.IsNullOrEmpty(name) || !_variables.ContainsKey(name)){
                return;
            }
            var type = SelectType(value);

            _variables.Remove(name);
            _variables[name] = new Variable(name, type, value);
        }

        private static VariableType SelectType(object value) {
            if (value is string) {
                return VariableType.String;
            }
            if (value is float || value is double) {
                return VariableType.Float;
            }
            if (value is long || value is ulong || value is int || value is uint || value is short || value is ushort) {
                return VariableType.Number;
            }
            if (value is Color4) {
                return VariableType.Color;
            }
            if (value is bool) {
                return VariableType.Bool;
            }
            if (value is Vector3) {
                return VariableType.Vector;
            }
            return VariableType.Unknown;
        }

        public void SaveScript(string filename = null) {
            if (string.IsNullOrEmpty(filename)) {
                filename = FileName;
            }
            using (var writer = new StreamWriter(filename)) {
                writer.WriteLine("#begin");

                foreach (var variable in _variables) {
                    switch (variable.Value.Type) {
                        case VariableType.Bool:
                            writer.WriteLine("{0} bool {1}", variable.Key, (bool)variable.Value.Data ? "true" : "false");
                            break;
                        case VariableType.Color:
                            var c = ((Color4) variable.Value.Data);
                            writer.WriteLine("{0} color {1} {2} {3} {4}", variable.Key, c.Red, c.Green, c.Blue, c.Alpha);
                            break;
                        case VariableType.Float:
                            writer.WriteLine("{0} float {1}", variable.Key, (float)variable.Value.Data);
                            break;
                        case VariableType.Number:
                            writer.WriteLine("{0} number {1}", variable.Key, (long)variable.Value.Data);
                            break;
                        case VariableType.String:
                            writer.WriteLine("{0} string {1}", variable.Key, variable.Value.Data);
                            break;
                        case VariableType.Vector:
                            var v = ((Vector3) variable.Value.Data);
                            writer.WriteLine("{0} vector {1} {2} {3}", variable.Key, v.X, v.Y, v.Z);
                            break;
                        case VariableType.Unknown:
                            writer.WriteLine("{0} unknown {1}", variable.Key, variable.Value.Data);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                writer.WriteLine("#end");
            }
                
        }

        public bool GetBool(string variable) {
            if (string.IsNullOrEmpty(variable) || !_variables.ContainsKey(variable)) {
                return default(bool);
            }
            return (bool) _variables[variable].Data;
        }
        public Color4 GetColor(string variable) {
            if (string.IsNullOrEmpty(variable) || !_variables.ContainsKey(variable)) {
                return default(Color4);
            }
            return  (Color4) _variables[variable].Data;
        }
        public float GetFloat(string variable) {
            if (string.IsNullOrEmpty(variable) || !_variables.ContainsKey(variable)) {
                return default(float);
            }
            return (float)_variables[variable].Data;
        }
        public long GetNumber(string variable) {
            if (string.IsNullOrEmpty(variable) || !_variables.ContainsKey(variable)) {
                return default(long);
            }
            return (long) _variables[variable].Data;
        }
        public string GetString(string variable) {
            if (string.IsNullOrEmpty(variable) || !_variables.ContainsKey(variable)) {
                return default(string);
            }
            return _variables[variable].Data as string;
        }
        public Vector3 GetVector(string variable) {
            if (string.IsNullOrEmpty(variable) || !_variables.ContainsKey(variable)) {
                return default(Vector3);
            }
            return (Vector3) _variables[variable].Data;
        }
        public object GetUnknown(string variable) {
            if (string.IsNullOrEmpty(variable) || !_variables.ContainsKey(variable)) {
                return default(object);
            }
            return _variables[variable].Data;
        }
    }
}