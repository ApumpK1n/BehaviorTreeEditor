
namespace Pumpkin.AI.BehaviorTree
{
    public interface IBTGraphNodeData
    {
        string Name { get; set; }

        BTNodeType NodeType { get; }

        (BTPortCapacity In, BTPortCapacity Out) Capacity { get; }
    }
}


