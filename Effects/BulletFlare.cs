using UnityEngine;

namespace Effects
{
	public sealed class BulletFlare : MonoBehaviour
	{
		[SerializeField, Min(0f)] private float _duration = 1f;

		private void Update()
		{
			_remainingTime -= Time.deltaTime;
			if (_remainingTime > 0f) return;

			gameObject.SetActive(false);
		}

		public void Enable()
		{
			gameObject.SetActive(true);
			_remainingTime = _duration;
		}

		private float _remainingTime;
	}
}