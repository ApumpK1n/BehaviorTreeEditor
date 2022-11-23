using System;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    [Serializable]
    public class ActionScriptProperty : SerializableProperty
    {
        public string ActionScriptType;

        private ActionScript m_ActionScript;

        public override void Init(GameObject actor)
        {

            Type type = Type.GetType(ActionScriptType);
            m_ActionScript = Activator.CreateInstance(type) as ActionScript;

            m_ActionScript.Init(actor);
        }

        public override BTNodeState Tick()
        {
            return m_ActionScript.Tick();
        }
    }
}
