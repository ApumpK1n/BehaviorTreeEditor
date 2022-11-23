using System;

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

        public static BTBaseNode CreateAction(string type)
        {
            Type propertyType = Type.GetType(type);

            var nodeCreationMethodName = nameof(BTNodeFactory.CreateNodeGeneric);
            var methodInfo = typeof(BTNodeFactory).GetMethod(nodeCreationMethodName);
            var genericMethodInfo = methodInfo.MakeGenericMethod(propertyType);

            object node = genericMethodInfo.Invoke(null, new object[] { }); ;
            return node as BTBaseNode;
        }

        public static BTBaseNode CreateNodeGeneric<T>() where T : SerializableProperty
        {
            return new BTAction<T>();
        }
    }
}