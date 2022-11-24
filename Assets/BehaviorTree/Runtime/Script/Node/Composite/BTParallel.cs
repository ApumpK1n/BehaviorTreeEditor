using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTParallel<T> : BTComposite<T> where T : ParallelProperty
    {
        public override BTNodeType NodeType => BTNodeType.Parallel;

        public override BTNodeState Tick()
        {
            int failCount = 0;
            for (int i = 0; i < m_Children.Length; i++)
            {
                var res = m_Children[i].Tick();

                if (res == BTNodeState.FAILURE)
                {
                    failCount += 1;
                }
            }

            BTNodeState nodeState = BTNodeState.SUCCESS;

            switch (m_Property.Strategy)
            {
                case Strategy.FAIL_ON_ONE:
                    nodeState = failCount > 0 ? BTNodeState.FAILURE : BTNodeState.SUCCESS;
                    break;
                case Strategy.FAIL_ON_ALL:
                    nodeState = failCount >= m_Children.Length ? BTNodeState.FAILURE : BTNodeState.SUCCESS;
                    break;
            }

            return nodeState;
        }
    }

}
