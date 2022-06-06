using System;
using Components;
using UnityEngine;

namespace Collection
{
	public abstract class CollectibleBase : MonoBehaviour, ICollectible
	{
		[SerializeField] private DeactivationMode _afterCollected = DeactivationMode.Destroy;

		public void GetCollected(IComponentProvider componentProvider)
		{
			OnGetCollected(componentProvider);
			Destroy();
		}

		protected abstract void OnGetCollected(IComponentProvider componentProvider);

		private void Destroy()
		{
			switch (_afterCollected)
			{
				case DeactivationMode.Destroy:
					Destroy(gameObject);
					break;
				case DeactivationMode.Disable:
					gameObject.SetActive(false);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private enum DeactivationMode
		{
			Destroy,
			Disable
		}
	}
}