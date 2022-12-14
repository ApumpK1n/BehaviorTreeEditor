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

        public static string DefaultNodeConfigPath = "Assets/BehaviorTree/Editor/Assets/Config/New BT Graph Node Config.asset";

        public static string GraphNodeStyleSheetPath = "Assets/BehaviorTree/Editor/Assets/Stylesheets/BTGraphNode.uss";

        public static string GraphViewStyleSheetPath = "Assets/BehaviorTree/Editor/Assets/Stylesheets/DefaultEditorWindowGrid.uss";
    }
}
