using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elements
{
    [Serializable]
    public class SerializableMessage
    {
        public string name;
        public int position, startPosition, endPosition;       

        public SerializableMessage(Message message) {
            this.name = message.Name;
            this.position = message.Position;
            this.startPosition = message.Start.Position;
            this.endPosition = message.End.Position;
        }

    }
}
