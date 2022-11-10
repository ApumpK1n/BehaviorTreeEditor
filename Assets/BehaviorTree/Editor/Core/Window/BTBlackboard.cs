using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;

namespace Pumpkin.AI.BehaviorTree
{

	public class BTBlackboard : Blackboard
	{
		private Dictionary<string, IBlackboardItem> m_ItemDict;

		public BTBlackboard(GraphView graphView = null) : base(graphView)
		{
			m_ItemDict = new Dictionary<string, IBlackboardItem>();
		}

		public bool Add<T>(string key, T value)
		{
			if (m_ItemDict.TryGetValue(key, out var _))
			{
				return false;
			}

			m_ItemDict.Add(key, new BlackboardItem<T>(value));
			return true;
		}

		public bool Update<T>(string key, T value)
		{
			if (!m_ItemDict.TryGetValue(key, out var _))
			{
				return false;
			}

			m_ItemDict[key] = value as IBlackboardItem;
			return true;
		}

		public bool Remove(string key)
		{
			if (!m_ItemDict.TryGetValue(key, out var _))
			{
				return false;
			}

			m_ItemDict.Remove(key);
			return true;
		}
	}

	public interface IBlackboardItem
	{ }

	public class BlackboardItem<T> : IBlackboardItem
	{
		private T _value;

		public BlackboardItem(T value)
		{
			_value = value;
		}

		public T Value { get => _value; set => _value = value; }
		public string TypeName => typeof(T).ToString();
	}
}