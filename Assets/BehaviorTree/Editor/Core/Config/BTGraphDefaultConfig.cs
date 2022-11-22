using System;

namespace Pumpkin.AI.BehaviorTree
{
    public static class BTGraphDefaultConfig
    {
        public static string GraphRootName = "Root";

        public static string GraphSelectorName = "Selector";

        public static string GraphSequencerName = "Sequencer";

        public static string GraphParallelName = "Parallel";

        public static string GraphActionName = "Action";

        public static string GraphNullName = "Null";

        public static string DefaultActionConfigPath = "Config/Default BT Graph Action Config";

        public static string DefaultNodeConfigPath = "Assets/BehaviorTree/Editor/Resources/Config/New BT Graph Node Config.asset";
    }
}
