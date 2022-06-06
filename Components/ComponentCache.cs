using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Components
{
	public sealed class ComponentCache : MonoBehaviour, IComponentProvider
	{
		[SerializeField] private bool _searchInInactiveChildren = false;

		public T Get<T>() where T : class
		{
			var type = typeof(T);
			if (_cache.TryGetValue(type, out var componentObject))
				return (T) componentObject;

			componentObject = GetComponentInChildren<T>(_searchInInactiveChildren);
			if ((Object) componentObject == null)
				throw new InvalidOperationException(
					$"There is no component of type {type} in children of {gameObject}.");
			_cache[type] = componentObject;

			return (T) componentObject;
		}

		private readonly IDictionary<Type, object> _cache = new Dictionary<Type, object>();
	}
}