using Components;
using UnityEngine;

namespace Collection
{
	public class CollectorTrigger : MonoBehaviour
	{
		private void OnTriggerEnter(Collider other)
		{
			if (!other.TryGetComponent(out ICollectible collectible)) return;
			collectible.GetCollected(_componentProvider);
		}

		public void Construct(IComponentProvider componentProvider)
		{
			_componentProvider = componentProvider;
		}

		private IComponentProvider _componentProvider;
	}
}