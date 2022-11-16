using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTParallel : BTComposite
    {
        public override BTNodeType NodeType => BTNodeType.Parallel;

        public override BTNodeState Tick()
        {
            for (int i = 0; i < m_Children.Length; i++)
            {
                var res = m_Children[i].Tick();

                if (res != BTNodeState.SUCCESS)
                {
                    return res;
                }
            }

            return BTNodeState.SUCCESS;
        }
    }

}
