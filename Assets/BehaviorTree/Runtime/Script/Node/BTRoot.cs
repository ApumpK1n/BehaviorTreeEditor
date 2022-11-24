using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTRoot : BTNull
    {
        public new BTNodeType NodeType => BTNodeType.Root;
    }
}

