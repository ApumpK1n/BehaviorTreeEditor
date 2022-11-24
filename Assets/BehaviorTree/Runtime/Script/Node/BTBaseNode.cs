﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTBaseNode: INode
    {
        public virtual BTNodeType NodeType => throw new NotImplementedException();

        protected object m_Property;

        public virtual bool Init(INode[] children, GameObject actor, string json, Type propertyType)
        {
            if (String.IsNullOrEmpty(json))
            {
                m_Property = Util.UnpackPropertyJson(json, propertyType);
            }
            return true;
        }

        public virtual BTNodeState Tick()
        {
            throw new NotImplementedException();
        }
    }
}
