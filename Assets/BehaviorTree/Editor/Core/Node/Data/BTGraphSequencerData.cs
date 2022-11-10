
using System;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTGraphSequencerData : IBTGraphNodeData
    {
        private string m_Name;

        public string Name 
        { 
            get 
            {
                if (String.IsNullOrEmpty(m_Name))
                {
                    m_Name = BTGraphDefaultConfig.GraphSequencerName;
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

        public BTNodeType NodeType => BTNodeType.Sequencer;

        public (BTPortCapacity In, BTPortCapacity Out) Capacity => (BTPortCapacity.Single, BTPortCapacity.Multi);
    }
}

