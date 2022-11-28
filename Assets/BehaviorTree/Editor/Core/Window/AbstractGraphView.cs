using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEditor;

namespace Pumpkin.AI.BehaviorTree
{
    public abstract class AbstractGraphView : GraphView
    {
        protected AbstractGraphView()
        {
            
            StyleSheet style = AssetDatabase.LoadAssetAtPath<StyleSheet>(BTGraphDefaultConfig.GraphViewStyleSheetPath);

            styleSheets.Add(style);

            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var grid = new GridBackground();
            grid.StretchToParentSize();
            Insert(0, grid);
        }

        protected void ClearNodesAndEdges()
        {
            nodes.ForEach(node => RemoveElement(node));
            edges.ForEach(edge => RemoveElement(edge));
        }
    }
}

