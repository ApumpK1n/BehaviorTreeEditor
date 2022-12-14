using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    public enum BTNodeState
    {
        StandBy,
        ACTIVE,
        INACTIVE,
    }

    public enum BTNodeType
    {
        Root,
        Sequencer,
        Selector,
        Parallel,
        Null,
        Action,
        DecoratorRepeat,
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
        Decorator,
        Custom,

    }

    public interface INode
    {

        public delegate void NodeStatusChangedEventHandler(INode sender);

        public event NodeStatusChangedEventHandler NodeStatusChanged;
        public BTNodeType NodeType { get; }
        public BTNodeState CurrentState { get; }
        public string Guid { get; set; }
        public Clock Clock { get; }
        public INode[] Children { get; }

        public bool ExitSuccess { get; set; }
        public bool Init(INode[] children, GameObject actor, string json, Type propertyType);

        public void SetParent(INode parent);

        public void Enter();

        public void Execute();
        public void Exit(bool success);

        public void OnExit();

        public void ChildExited(INode child, bool succeeded);

        public void SetClock(Clock clock);
    }
}

