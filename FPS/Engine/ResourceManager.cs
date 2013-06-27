using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Engine {
    public delegate Resource<T> CreateResourceFunc<T>(string name, string path);

    public class ResourceManager<T> where T:class {
        private List<Resource<T>> _resources;
        private CreateResourceFunc<T> CreateResource { get; set; }

        public List<Resource<T>> Resources { get { return _resources; } }

        public ResourceManager( CreateResourceFunc<T> func=null ) {
            _resources = new List<Resource<T>>();
            CreateResource = func;
        }
        ~ResourceManager() {
            Resources.Clear();
        }
        public Resource<T> Add(string name, string path = "./") {
            if (Resources == null || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(path)) {
                return null;
            }
            var element = GetElement(name, path);
            if (element != null) {
                element.IncRef();
                return element;
            }
            var resource = CreateResource != null ? CreateResource(name, path) : new Resource<T>(name, path);
            Resources.Add(resource);
            return resource;
        }
        public void Remove(Resource<T> resource) {
            if (resource == null || Resources == null) {
                return;
            }
            resource.DecRef();
            if (resource.RefCount == 0) {
                Resources.Remove(resource);
            }
        }
        public void Clear() {
            if (Resources != null) {  
                Resources.Clear();
            }
        }

        private Resource<T> GetElement(string name, string path = "./") {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(path) || _resources == null) {
                return null;
            }
            if (_resources.Count == 0) {
                return null;
            }
            return _resources.FirstOrDefault(r => r.Name == name && r.Path == path);
        }
    }

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
