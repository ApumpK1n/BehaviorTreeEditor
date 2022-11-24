using System;

namespace Pumpkin.AI.BehaviorTree
{
    public static class BTNodeFactory
    {
        //public static INode CreateGeneric(BTNodeType nodeType, string propetyType)
        //{
        //    Type propertyType = Type.GetType(propetyType);

        //    var nodeCreationMethodName = nameof(BTNodeFactory.CreateNodeGeneric);
        //    var methodInfo = typeof(BTNodeFactory).GetMethod(nodeCreationMethodName);
        //    var genericMethodInfo = methodInfo.MakeGenericMethod(propertyType);

        //    object node = genericMethodInfo.Invoke(null, new object[] { nodeType }); ;
        //    return node as INode;
        //}

        public static INode CreateNode(BTNodeType nodeType)
        {
            switch (nodeType)
            {
                case BTNodeType.Root:
                    return new BTRoot();
                case BTNodeType.Sequencer:
                    return new BTSequencer();
                case BTNodeType.Selector:
                    return new BTSelector();
                case BTNodeType.Parallel:
                    return new BTParallel();
                case BTNodeType.Action:
                    return new BTAction();
                case BTNodeType.DecoratorRepeat:
                    return new BTDecoratorRepeat();
                default:
                    return new BTNull();
            }
        }

            //public static INode CreateNodeGeneric<T>(BTNodeType nodeType) where T : SerializableProperty
            //{
            //    switch (nodeType)
            //    {
            //        case BTNodeType.Root:
            //            return new BTRoot<T>();
            //        case BTNodeType.Sequencer:
            //            return new BTSequencer<T>();
            //        case BTNodeType.Selector:
            //            return new BTSelector<T>();
            //        case BTNodeType.Parallel:
            //            return new BTParallel<ParallelProperty>();
            //        case BTNodeType.Action:
            //            return new BTAction<T>();
            //        default:
            //            return new BTNull<T>();
            //    }


            //}
        }
}