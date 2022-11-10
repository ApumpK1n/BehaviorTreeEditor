using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTGraphView : AbstractGraphView
    {
        private Vector2 DefaultRootSpawnPos = new Vector2(100f, 300f);

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<Port>();

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
