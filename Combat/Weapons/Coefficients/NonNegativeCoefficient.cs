using UnityEngine;

namespace Combat.Weapons.Coefficients
{
	public class NonNegativeCoefficient : MonoBehaviour
	{
		[SerializeField, Min(0f)] private float _defaultValue = 1f;

		public float Value
		{
			get => _value;
			set => _value = Mathf.Max(value, 0f);
		}

		private void OnEnable()
		{
			Value = _defaultValue;
		}

		private float _value;
	}
}