using System;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTGraphActionData<T> : IBTGraphNodeData where T : SerializableProperty
    {
        private string m_Name;

        public string Name
        {
            get
            {
                if (String.IsNullOrEmpty(m_Name))
                {
                    m_Name = BTGraphDefaultConfig.GraphActionName;
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

        public BTNodeType NodeType => BTNodeType.Action;

        public (BTPortCapacity In, BTPortCapacity Out) Capacity => (BTPortCapacity.Single, BTPortCapacity.None);

        public Type PropertyType => typeof(T);
    }
}
