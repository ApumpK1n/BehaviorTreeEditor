using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTRoot : BTNull
    {
        public override BTNodeType NodeType => BTNodeType.Root;
    }
}

