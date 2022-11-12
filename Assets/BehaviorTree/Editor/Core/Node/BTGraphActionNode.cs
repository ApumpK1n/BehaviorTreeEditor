using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTGraphActionNode<T> : BTGraphNode<BTGraphActionData<T>>, ISavable where T : SerializableProperty, new()
    {

        public BTGraphActionNode(Vector2 pos, string guid = "") : base(pos, guid)
        {
            mainContainer.style.backgroundColor = new StyleColor(new Color(50f / 255f, 50f / 255f, 50f / 255f));

            DrawProperties();
        }

        private void DrawProperties()
        {

            VisualElement container = new VisualElement();
            FieldInfo[] fieldInfoList = m_NodeData.PropertyType.GetFields();

            T defaultPropety = new T();

            foreach (var fieldInfo in fieldInfoList)
            {
                VisualElement childContainer = new VisualElement();
                childContainer.style.height = 20;
                childContainer.style.flexDirection = FlexDirection.Row;
                childContainer.style.marginTop = 5;
                childContainer.style.marginBottom = 5;
                childContainer.style.marginRight = 5;
                childContainer.style.marginLeft = 5;

                var label = new Label(fieldInfo.Name);
                label.style.width = 70;
                label.style.unityTextAlign = TextAnchor.MiddleLeft;
                label.style.fontSize = 12;
                label.style.whiteSpace = WhiteSpace.Normal;
                childContainer.Add(label);

                VisualElement field = DrawPropField(fieldInfo, defaultPropety);
                StylizePropField(field);
                childContainer.Add(field);

                //indPropDataFn += bindPropFieldFn;

                container.Add(childContainer);
            }
        }

        private VisualElement DrawPropField(FieldInfo fieldInfo, object propFieldData)
        {
            var type = fieldInfo.FieldType;

            if (type == typeof(string))
            {
                //if (fieldInfo.Name.Contains("Tag"))
                //{
                //    var tagField = new TagField(string.Empty, "Untagged");
                //    tagField.value = (string)fieldInfo.GetValue(propFieldData);
                //    return (StylizePropField(tagField), prop => fieldInfo.SetValue(prop, tagField.value));
                //}

                var field = new TextField(string.Empty, 200, true, false, '*')
                {
                    value = (string)fieldInfo.GetValue(propFieldData)
                };
                return field;
            }

            if (type == typeof(int))
            {
                var field = new IntegerField
                {
                    value = (int)fieldInfo.GetValue(propFieldData)
                };
                return field;
            }

            if (type == typeof(float))
            {
                var field = new FloatField
                {
                    value = (float)fieldInfo.GetValue(propFieldData)
                };
                return field;
            }

            if (type == typeof(bool))
            {
                var field = new Toggle
                {
                    value = (bool)fieldInfo.GetValue(propFieldData)
                };
                return field;
            }

            if (type == typeof(Vector3))
            {
                var field = new Vector3Field
                {
                    value = (Vector3)fieldInfo.GetValue(propFieldData)
                };
                return field;
            }

            if (typeof(ScriptableObject).IsAssignableFrom(type) || type.IsInterface)
            {
                return new ObjectField() { objectType = type };
            }

            return new Label($"Unsupported type {type}");
        }

        private void StylizePropField(VisualElement field)
        {
            field.style.width = 100;
        }

        public override void Save(BehaviorTreeDesignContainer designContainer)
        {
            //designContainer.AddNodeData(
            //   new GraphSerializableNodeData(GetPosition().position, m_Guid, GetParentGuid(inputContainer), m_NodeData.NodeType));
        }
    }
}
