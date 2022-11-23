using System;

namespace Pumpkin.AI.BehaviorTree
{
    [Serializable]
    public class ActionWaitProperty : SerializableProperty
    {

        public int Delay;


        public override BTNodeState Tick()
        {
            return base.Tick();
        }
    }
}
