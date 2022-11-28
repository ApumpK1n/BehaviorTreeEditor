using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTAction: BTBaseNode
    {
        public new BTNodeType NodeType => BTNodeType.Action;

        private SerializableProperty m_Property;

        public override bool Init(INode[] children, GameObject actor, string json, Type propertyType)
        {
            Assert.IsTrue(children.Length < 1, "Action Init child count > 0!");
            if (children.Length > 0) 
            {
                Debug.LogWarning("BTAction Init action child count > 0!");
                return false;
            }

            m_Property = (SerializableProperty)Util.UnpackPropertyJson(json, propertyType);
            
            m_Property.Init(actor);

            return true;
        }

        public override void Execute()
        {
            m_Property.Execute();

            Exit(true);
        }
    }
}
