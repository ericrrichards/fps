using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Engine {
    public class Resource<T> {
        public Resource(string name, string path = "./") {
            if (!string.IsNullOrEmpty(name)) {
                Name = name;
            }
            if (!string.IsNullOrEmpty(path)) {
                Path = path;
            }
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(path)) {
                FileName = System.IO.Path.Combine(path, name);
            }
            RefCount = 1;
        }
        ~Resource() {
        }

        public string Name { get; private set; }
        public string Path { get; private set; }
        public string FileName { get; private set; }

        public void IncRef() {
            RefCount++;
        }
        public void DecRef() {
            RefCount--;
        }

        public ulong RefCount { get; private set; }
    }

    
}
