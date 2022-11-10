namespace Pumpkin.AI.BehaviorTree
{
    public static class BTNodeFactory
    {
        public static BTBaseNode Create(BTNodeType type)
        {
            switch (type)
            {
                case BTNodeType.Root:
                    return new BTRoot();
                case BTNodeType.Sequencer:
                    return new BTSequencer();
                case BTNodeType.Selector:
                    return new BTSelector();
                default:
                    return new BTNull();
            }
        }

        //public static BTBaseNode CreateAction(BTBaseTask task, string guid)
        //{
        //    return new BTLeaf(task, guid);
        //}
    }
}