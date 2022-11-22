using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    public enum BTNodeState
    {
        SUCCESS,
        FAILURE,
        RUNNING
    }

    public enum BTNodeType
    {
        Root,
        Sequencer,
        Selector,
        Parallel,
        Null,
        Action
    }

    public enum Strategy
    {

        FAIL_ON_ONE,
        FAIL_ON_ALL,
    }

    public enum NodeBelongTo
    {
        Hide,
        Base,
        Composite,
        Action,
        Custom,
    }

    public abstract class BTBaseNode
    {
        public abstract BTNodeType NodeType { get; }
        public abstract bool Init(BTBaseNode[] children, GameObject actor);
        public abstract BTNodeState Tick();

    }
}

