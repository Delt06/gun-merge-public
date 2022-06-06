using UnityEngine;

namespace Combat.Weapons.Coefficients
{
	public sealed class ConfigurableCriticalDamage : MonoBehaviour, ICriticalDamage
	{
		[SerializeField, Min(0f)] private float _defaultCoefficient = 2f;
		[SerializeField, Range(0f, 1f)] private float _defaultProbability = 0.1f;

		public float Coefficient
		{
			get => _coefficient;
			set => _coefficient = Mathf.Max(value, 0f);
		}

		public float Probability
		{
			get => _probability;
			set => _probability = Mathf.Clamp01(value);
		}

		private void OnEnable()
		{
			Coefficient = _defaultCoefficient;
			Probability = _defaultProbability;
		}

		private float _coefficient;
		private float _probability;
	}
}