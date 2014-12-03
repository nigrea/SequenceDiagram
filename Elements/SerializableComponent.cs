using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elements
{
    [Serializable]
    public class SerializableComponent
    {
        public string name;
        public int position;

        public SerializableComponent(Component component) {
            this.name = component.Name;
            this.position = component.Position;
        
        }

    }
}
