using UnityEngine;

namespace Animations
{
	public sealed class PositionOscillationMovement : MonoBehaviour
	{
		[SerializeField] private Vector3 _amplitude = Vector3.up;
		[SerializeField, Min(0f)] private float _period = 1f;

		private void Update()
		{
			var lifetime = Time.time - _enabledTime;
			var periods = lifetime / _period;
			var phase = periods * Mathf.PI * 2f;
			var offset = Mathf.Sin(phase) * _amplitude;
			transform.localPosition = _initialLocalPosition + offset;
		}

		private void OnEnable()
		{
			transform.localPosition = _initialLocalPosition;
			_enabledTime = Time.time;
		}

		private void Awake()
		{
			_initialLocalPosition = transform.localPosition;
		}

		private float _enabledTime;
		private Vector3 _initialLocalPosition;
	}
}