using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Pumpkin.AI.BehaviorTree
{
    public class BehaviorTreeDebugWindow : GraphViewEditorWindow
    {
        public static BehaviorTreeDebugWindow Instance;

        private DataManager m_DataManager;

        private BTDebugGraphView m_BTGraphView;

        [MenuItem("Window/Pumpkin/Behavior Tree Debug")]
        public static void Init()
        {
            Instance = GetWindow<BehaviorTreeDebugWindow>("Behavior Tree Debug");

        }

        private void CreateGUI()
        {
            m_DataManager = new DataManager();
            m_BTGraphView = CreateGraphView();

            rootVisualElement.Add(m_BTGraphView);
        }

        public void BuildBehaviorTree(BehaviorTree tree)
        {
            m_BTGraphView.UpdateView(tree);
        }

        private BTDebugGraphView CreateGraphView()
        {
            var emptyGraphView = new BTDebugGraphView();
            emptyGraphView.SetDataManager(m_DataManager);
            emptyGraphView.StretchToParentSize();
            return emptyGraphView;
        }
    }
}
