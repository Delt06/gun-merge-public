using System;
using UnityEngine;

namespace Events
{
	[CreateAssetMenu(menuName = AssetPath + "No Args")]
	public sealed class GlobalEvent : ScriptableObject
	{
		public void Raise() => InnerEvent?.Invoke();

		public void AddListener(Action action) => InnerEvent += action;
		public void RemoveListener(Action action) => InnerEvent -= action;

		private event Action InnerEvent;

		internal const string AssetPath = "Event/";
	}

	public abstract class GlobalEvent<T> : ScriptableObject
	{
		public void Raise(T args) => InnerEvent?.Invoke(args);

		public void AddListener(Action<T> action) => InnerEvent += action;
		public void RemoveListener(Action<T> action) => InnerEvent -= action;

		private event Action<T> InnerEvent;
	}
}