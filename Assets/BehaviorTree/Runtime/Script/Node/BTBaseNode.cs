using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTBaseNode: INode
    {
        public virtual BTNodeType NodeType => throw new NotImplementedException();

        public BTNodeState CurrentState => m_CurrentState;

        public Clock Clock => m_Clock;
        public string Guid { get; set; }

        public INode[] Children => m_Children;

        public bool ExitSuccess { get; set; }

        protected INode m_Parent;
        protected INode[] m_Children;

        protected BTNodeState m_CurrentState = BTNodeState.StandBy;

        protected Clock m_Clock;

        public event INode.NodeStatusChangedEventHandler NodeStatusChanged;

        public virtual bool Init(INode[] children, GameObject actor, string json, Type propertyType)
        {
            m_Children = children;

            foreach (INode node in m_Children)
            {
                node.SetParent(this);
            }
            return true;
        }



        public void Enter()
        {
            Assert.AreNotEqual(m_CurrentState, BTNodeState.ACTIVE, "can not start active node");
            m_CurrentState = BTNodeState.ACTIVE;

            NodeStatusChanged?.Invoke(this);
            Execute();
        }

        public virtual void Execute()
        {
            
        }

        public void Exit(bool success)
        {
            Assert.AreNotEqual(m_CurrentState, BTNodeState.INACTIVE, "can not Exit inactive node");

            m_CurrentState = BTNodeState.INACTIVE;
            ExitSuccess = success;

            NodeStatusChanged?.Invoke(this);
            if (m_Parent != null)
            {
                m_Parent.ChildExited(this, success);
            }
        }

        public virtual void OnExit()
        {
            throw new NotImplementedException();
        }

        public void SetParent(INode parent)
        {
            m_Parent = parent;
        }

        public void ChildExited(INode child, bool succeeded)
        {

            OnChildExited(child, succeeded);
        }

        protected virtual void OnChildExited(INode child, bool succeeded)
        {

        }

        protected virtual void OnNodeStatusChanged(INode sender)
        {
            NodeStatusChanged?.Invoke(sender);
        }

        public void SetClock(Clock clock)
        {
            m_Clock = clock;
        }
    }
}
