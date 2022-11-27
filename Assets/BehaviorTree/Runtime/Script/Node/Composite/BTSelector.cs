using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTSelector: BTComposite
    {
        public override BTNodeType NodeType => BTNodeType.Selector;

        private int m_CurrentIndex = -1;

        public override void Execute()
        {
            foreach (INode child in m_Children)
            {
                Assert.AreEqual(child.CurrentState, BTNodeState.INACTIVE);
            }

            m_CurrentIndex = -1;

            ProcessChildren();
        }

        private void ProcessChildren()
        {
            if (++m_CurrentIndex < m_Children.Length)
            {

                m_Children[m_CurrentIndex].Enter();
            }
            else
            {
                Exit(false);
            }
        }

        public override BTNodeState Tick()
        {
            for (int i = 0; i < m_Children.Length; i++)
            {
                var res = m_Children[i].Tick();

                if (res != BTNodeState.FAILURE)
                {
                    return res;
                }
            }

            return BTNodeState.FAILURE;
        }
    }


}
