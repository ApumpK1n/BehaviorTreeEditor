using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTAction<T> : BTBaseNode where T : SerializableProperty
    {
        public override BTNodeType NodeType => BTNodeType.Action;

        private T m_Task;

        public override bool Init(BTBaseNode[] children, GameObject actor, string json)
        {
            if (children.Length > 0) 
            {
                Debug.LogWarning("BTAction Init action child count > 0!");
                return false;
            }

            m_Task = Util.UnpackPropertyJson<T>(json);

            m_Task.Init(actor);

            return true;
        }

        public override BTNodeState Tick()
        {
            return m_Task.Tick();
        }
    }
}
