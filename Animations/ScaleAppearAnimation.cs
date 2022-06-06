using DG.Tweening;
using UnityEngine;

namespace Animations
{
	public sealed class ScaleAppearAnimation : MonoBehaviour
	{
		[SerializeField, Min(0f)] private float _delay = 0f;
		[SerializeField, Min(0f)] private float _duration = 1f;
		[SerializeField] private Ease _ease = Ease.Linear;
		[SerializeField, Min(0f)] private float _overshoot = 1.7f;

		private void OnEnable()
		{
			_sequence?.Kill();
			transform.localScale = Vector3.zero;

			_sequence = DOTween.Sequence()
				.AppendInterval(_delay)
				.Append(transform.DOScale(_initialScale, _duration).SetEase(_ease, _overshoot))
				.OnComplete(_onComplete);
		}

		private void OnDisable()
		{
			_sequence?.Kill();
		}

		private void Awake()
		{
			_initialScale = transform.localScale;
			_onComplete = () => _sequence = null;
		}

		private Vector3 _initialScale;
		private Sequence _sequence;
		private TweenCallback _onComplete;
	}
}