using System;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    [Serializable]
    public class ActionScriptProperty : SerializableProperty
    {
        public string ActionScriptType;
        public ExecuteType ExecuteScriptType;

        private ActionScript m_ActionScript;

        public enum ExecuteType
        {
            Single,
            Tick,
        }
        public override void Init(GameObject actor, INode parent)
        {
            base.Init(actor, parent);

            Type type = Type.GetType(ActionScriptType);
            m_ActionScript = Activator.CreateInstance(type) as ActionScript;

            m_ActionScript.Init(actor);
        }

        public override void Execute()
        {
            switch (ExecuteScriptType)
            {
                case ExecuteType.Single:
                    m_ActionScript.Execute();
                    m_Parent.Exit(true);
                    break;
                case ExecuteType.Tick:
                    bool done = m_ActionScript.Execute();
                    if (!done)
                    {
                        m_Parent.Clock.AddTimer(0, 0, TickScript);
                    }
                    else
                    {
                        m_Parent.Exit(true);
                    }
                    break;
            }
 
        }

        private void TickScript()
        {
            bool done = m_ActionScript.Execute();
            if (!done)
            {
                m_Parent.Clock.AddTimer(0, 0, TickScript);
            }
            else
            {
                m_Parent.Exit(true);
            }
        }

    }
}
