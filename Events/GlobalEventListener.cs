using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Events
{
	public abstract class GlobalEventListener : MonoBehaviour
	{
		[SerializeField, Required, AssetSelector]
		private GlobalEvent _globalEvent = default;

		protected void OnEnable()
		{
			_globalEvent.AddListener(_onEvent);
			OnEnabled();
		}

		protected virtual void OnEnabled() { }

		protected void OnDisable()
		{
			_globalEvent.RemoveListener(_onEvent);
			OnDisabled();
		}

		protected virtual void OnDisabled() { }

		protected void Awake()
		{
			_onEvent = OnEvent;
			OnAwaken();
		}

		protected virtual void OnAwaken() { }

		protected abstract void OnEvent();

		private Action _onEvent;
	}

	public abstract class GlobalEventListener<TArgs, TEvent> : MonoBehaviour where TEvent : GlobalEvent<TArgs>
	{
		[SerializeField, Required, AssetSelector]
		private TEvent _globalEvent = default;

		protected void OnEnable()
		{
			_globalEvent.AddListener(_onEvent);
			OnEnabled();
		}

		protected virtual void OnEnabled() { }

		protected void OnDisable()
		{
			_globalEvent.RemoveListener(_onEvent);
			OnDisabled();
		}

		protected virtual void OnDisabled() { }

		protected void Awake()
		{
			_onEvent = OnEvent;
			OnAwaken();
		}

		protected virtual void OnAwaken() { }

		protected abstract void OnEvent(TArgs args);

		private Action<TArgs> _onEvent;
	}
}