using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using System;
using System.Linq;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTEditorCreateNodeWindow : ScriptableObject, ISearchWindowProvider
    {
        private List<Type> taskTypes = new List<Type>();
        private Action<Node> OnEntrySelected;
        private Func<Vector2, Vector2> ContextToLocalMousePos;
        private Texture2D m_Indentation;

        private BTGraphActionConfig m_GraphActionConfig; // Action节点配置信息

        public void Init(Action<Node> entrySelectCallback, Func<Vector2, Vector2> contextToLocalMousePos)
        {
            OnEntrySelected = entrySelectCallback;
            ContextToLocalMousePos = contextToLocalMousePos;

            m_Indentation = new Texture2D(1, 1);
            m_Indentation.SetPixel(0, 0, new Color(0, 0, 0, 0));
            m_Indentation.Apply();

            m_GraphActionConfig = Resources.Load<BTGraphActionConfig>(BTGraphDefaultConfig.DefaultActionConfigPath);
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            var tree = new List<SearchTreeEntry>()
            {
                new SearchTreeGroupEntry(new GUIContent("Add Node")),
                new SearchTreeGroupEntry(new GUIContent("Composite"), 1),
                new SearchTreeEntry(new GUIContent(BTGraphDefaultConfig.GraphSequencerName, m_Indentation))
                {
                    userData = typeof(BTGraphSequencerData),
                    level = 2
                },
                new SearchTreeEntry(new GUIContent(BTGraphDefaultConfig.GraphSelectorName, m_Indentation))
                {
                    userData = typeof(BTGraphSelectorData),
                    level = 2
                },
                new SearchTreeGroupEntry(new GUIContent("Action"), 1),
               
            };

            if (m_GraphActionConfig != null && m_GraphActionConfig.ActionInfos.Count > 0)
            {
                foreach (var info in m_GraphActionConfig.ActionInfos)
                {
                    Type type = Type.GetType(info.PropertyType);
                    if (type != null)
                    {
                        SearchTreeEntry entry = new SearchTreeEntry(new GUIContent(info.Name, m_Indentation))
                        {
                            userData = type,
                            level = 2
                        };
                        tree.Add(entry);
                    }
                }
            }

           
            

            return tree;
        }

        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            var nodeSpawnPos = ContextToLocalMousePos(context.screenMousePosition);
            Type userDataType = searchTreeEntry.userData as Type;

            var nodeCreationMethodName =
                typeof(SerializableProperty).IsAssignableFrom(userDataType) ? nameof(BTGraphNodeFactory.CreateActionNodeGeneric)
                 : nameof(BTGraphNodeFactory.CreateNodeGeneric);
            var methodInfo = typeof(BTGraphNodeFactory).GetMethod(nodeCreationMethodName);
            var genericMethodInfo = methodInfo.MakeGenericMethod(userDataType);
            var node = genericMethodInfo.Invoke(null, new object[] { nodeSpawnPos});
            OnEntrySelected?.Invoke(node as Node);
            return true;
        }
    }

}

