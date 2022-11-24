using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTParallel: BTComposite
    {
        public override BTNodeType NodeType => BTNodeType.Parallel;

        private ParallelProperty m_Property;

        public override bool Init(INode[] children, GameObject actor, string json, Type propertyType)
        {
            bool state = base.Init(children, actor, json, propertyType);

            m_Property = Util.UnpackPropertyJson<ParallelProperty>(json);

            return state;
        }

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
