using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTAction<T> : BTBaseNode<T> where T : SerializableProperty 
    {
        public override BTNodeType NodeType => BTNodeType.Action;

        public override bool Init(INode[] children, GameObject actor, string json)
        {
            if (children.Length > 0) 
            {
                Debug.LogWarning("BTAction Init action child count > 0!");
                return false;
            }

            base.Init(children, actor, json);

            m_Property.Init(actor);

            return true;
        }

        public override BTNodeState Tick()
        {
            return m_Property.Tick();
        }
    }
}
