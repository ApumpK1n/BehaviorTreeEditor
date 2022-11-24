using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTRoot<T> : BTNull<T> where T : SerializableProperty
    {
        public override BTNodeType NodeType => BTNodeType.Root;
    }
}

