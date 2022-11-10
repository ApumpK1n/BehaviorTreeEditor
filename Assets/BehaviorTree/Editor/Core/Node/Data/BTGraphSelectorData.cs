
using System;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTGraphSelectorData : IBTGraphNodeData
    {
        private string m_Name;

        public string Name
        {
            get
            {
                if (String.IsNullOrEmpty(m_Name))
                {
                    m_Name = BTGraphDefaultConfig.GraphSelectorName;
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

        public BTNodeType NodeType => BTNodeType.Selector;

        public (BTPortCapacity In, BTPortCapacity Out) Capacity => (BTPortCapacity.Single, BTPortCapacity.Multi);
    }
}

