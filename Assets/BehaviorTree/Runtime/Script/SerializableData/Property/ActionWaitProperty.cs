using System;

namespace Pumpkin.AI.BehaviorTree
{
    [Serializable]
    public class ActionWaitProperty : SerializableProperty
    {

        public int Delay;


        public override void Execute()
        {

        }
    }
}
