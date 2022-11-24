using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTAction: INode
    {
        public BTNodeType NodeType => BTNodeType.Action;

        private SerializableProperty m_Property;

        public bool Init(INode[] children, GameObject actor, string json, Type propertyType)
        {
            if (children.Length > 0) 
            {
                Debug.LogWarning("BTAction Init action child count > 0!");
                return false;
            }

            m_Property = (SerializableProperty)Util.UnpackPropertyJson(json, propertyType);
            
            m_Property.Init(actor);

            return true;
        }

        public BTNodeState Tick()
        {
            return m_Property.Tick();
        }
    }
}
