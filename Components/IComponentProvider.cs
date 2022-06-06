using UnityEngine;

namespace Components
{
	public interface IComponentProvider
	{
		GameObject gameObject { get; }
		T Get<T>() where T : class;
	}
}