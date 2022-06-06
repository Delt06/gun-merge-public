using Combat;
using UnityEngine;
using UnityEngine.Events;

namespace Collection.Health
{
	public class Healing : MonoBehaviour
	{
		[SerializeField] private UnityEvent _onHealed = default;

		public float MaxHealth => _hasHealth.MaxHealth;

		public void Heal(float healingAmount)
		{
			_hasHealth.Health += healingAmount;
			_onHealed.Invoke();
		}

		public void Construct(IHasHealth hasHealth) => _hasHealth = hasHealth;

		private IHasHealth _hasHealth;
	}
}