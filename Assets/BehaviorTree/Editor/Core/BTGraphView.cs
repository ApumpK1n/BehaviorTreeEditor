using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTGraphView : AbstractGraphView
    {
        private UnityEngine.Vector2 defaultRootSpawnPos = new UnityEngine.Vector2(100f, 300f);

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new System.Collections.Generic.List<Port>();

            ports.ForEach(port =>
            {
                if (startPort.node != port.node && startPort != port)
                {
                    compatiblePorts.Add(port);
                }
            });

            return compatiblePorts;
        }

        public void UpdateView()
        {
           
        }
    }
}
