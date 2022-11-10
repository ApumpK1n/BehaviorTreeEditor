
using System;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTGraphRootData : IBTGraphNodeData
    {
        private string m_Name;

        public string Name
        {
            get
            {
                if (String.IsNullOrEmpty(m_Name))
                {
                    m_Name = BTGraphDefaultConfig.GraphRootName;
                    return m_Name;
                }
                else
                {
                    return m_Name;
                }
            }
            set
            {
                m_Name = value;
            }
        }

        public BTNodeType NodeType => BTNodeType.Root;

        public (BTPortCapacity In, BTPortCapacity Out) Capacity => (BTPortCapacity.None, BTPortCapacity.Single);
    }
}

