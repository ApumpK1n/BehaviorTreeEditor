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

        private DataManager m_DataMamager;

        public void Init(Action<Node> entrySelectCallback, Func<Vector2, Vector2> contextToLocalMousePos, DataManager dataManager)
        {
            OnEntrySelected = entrySelectCallback;
            ContextToLocalMousePos = contextToLocalMousePos;

            m_Indentation = new Texture2D(1, 1);
            m_Indentation.SetPixel(0, 0, new Color(0, 0, 0, 0));
            m_Indentation.Apply();

            m_GraphActionConfig = Resources.Load<BTGraphActionConfig>(BTGraphDefaultConfig.DefaultActionConfigPath);

            m_DataMamager = dataManager;
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            var tree = new List<SearchTreeEntry>()
            {
                new SearchTreeGroupEntry(new GUIContent("Add Node")),
               
            };

            

            foreach (NodeBelongTo belongTo in (NodeBelongTo[])Enum.GetValues(typeof(NodeBelongTo)))
            {
                if (belongTo == NodeBelongTo.Hide) continue;
                SearchTreeEntry entry =new SearchTreeGroupEntry(new GUIContent(belongTo.ToString()), 1);
                tree.Add(entry);

                if (m_DataMamager != null && m_DataMamager.NodeConfigFile.MainStyleProperties.Count > 0)
                {
                    foreach (var info in m_DataMamager.NodeConfigFile.MainStyleProperties)
                    {
                        if (info.NodeBelongTo != belongTo) continue;
                        SearchTreeEntry entryChild = new SearchTreeEntry(new GUIContent(info.Name, m_Indentation))
                        {
                                userData = info,
                                level = 2
                        };
                        tree.Add(entryChild);
                    }
                }
            }

            return tree;
        }

        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            var nodeSpawnPos = ContextToLocalMousePos(context.screenMousePosition);

            NodeProperty nodeProperty = searchTreeEntry.userData as NodeProperty;
            Node node = BTGraphNodeFactory.CreateNode(nodeSpawnPos, nodeProperty, null);

            OnEntrySelected?.Invoke(node);
            return true;
        }
    }

}

