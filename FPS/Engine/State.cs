using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine {
    public struct ViewerSetup{}


    public class State : Base {
        public State(ulong id = 0) {
            ID = id;
        }
        public virtual void Load(){}
        public virtual void Close(){}
        public virtual void RequestViewer(out ViewerSetup viewer){}
        public virtual void Update( float elapsed){}
        public virtual void Render(){}

        public ulong ID { get; private set; }
    }
}
