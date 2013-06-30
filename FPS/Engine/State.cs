using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SlimDX.Direct3D9;

namespace Engine {
    public class ViewerSetup{
        public ClearFlags ClearFlags { get; set; }

        public ViewerSetup() {
            ClearFlags = ClearFlags.All;
        }
    }


    public class State : Base {
        public State(ulong id = 0) {
            ID = id;
        }
        public virtual void Load(){}
        public virtual void Close(){}
        public virtual void RequestViewer(out ViewerSetup viewer){viewer = new ViewerSetup();}
        public virtual void Update( float elapsed){}
        public virtual void Render(){}

        public ulong ID { get; private set; }
    }
}
