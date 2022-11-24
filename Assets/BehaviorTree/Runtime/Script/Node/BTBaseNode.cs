using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTBaseNode<T> : INode where T : SerializableProperty
    {
        public virtual BTNodeType NodeType => throw new NotImplementedException();

        protected T m_Property;

        public virtual bool Init(INode[] children, GameObject actor, string json)
        {
            if (String.IsNullOrEmpty(json))
            {
                m_Property = Util.UnpackPropertyJson<T>(json);
            }
            return true;
        }

        public virtual BTNodeState Tick()
        {
            throw new NotImplementedException();
        }
    }
}
