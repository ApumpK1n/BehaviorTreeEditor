using System;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    [Serializable]
    public class ActionScriptProperty : SerializableProperty
    {
        public ActionMonoBehaviour MonoScript;


        public override void Init(GameObject actor)
        {
            MonoScript.Init(actor);
        }

        public override BTNodeState Tick()
        {
            return MonoScript.Tick();
        }
    }
}
