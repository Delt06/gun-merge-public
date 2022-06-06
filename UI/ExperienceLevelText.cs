using System;
using System.Text;
using Components;
using DG.Tweening;
using Progression.Experience;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace UI
{
	public sealed class ExperienceLevelText : MonoBehaviour
	{
		[SerializeField, Required] private TMP_Text _text = default;
		[SerializeField] private string _prefix = string.Empty;
		[SerializeField] private string _postfix = " LVL";
		[SerializeField, Min(0f)] private float _scaleUpDuration = 0.25f;
		[SerializeField, Min(0f)] private float _scaleDownDuration = 0.25f;
		[SerializeField] private Vector3 _scaleUp = Vector3.one * 1.5f;
		[SerializeField] private Ease _easeUp = Ease.Linear;
		[SerializeField] private Ease _easeDown = Ease.Linear;

		private void Start()
		{
			Refresh(true);
		}

		private void OnEnable()
		{
			Refresh(true);
			_experienceHolder.LevelChanged += _refresh;
		}

		private void OnDisable()
		{
			_experienceHolder.LevelChanged -= _refresh;
		}

		private void Awake()
		{
			_refresh = (sender, args) => Refresh();
			_onComplete = () => _sequence = null;
		}

		private void Refresh(bool force = false)
		{
			_stringBuilder.Clear()
				.Append(_prefix)
				.Append(_experienceHolder.Level)
				.Append(_postfix);
			_text.SetText(_stringBuilder);

			_sequence?.Kill();
			_sequence = null;

			_text.transform.localScale = Vector3.one;
			if (!force)
				_sequence = DOTween.Sequence()
					.Append(_text.transform.DOScale(_scaleUp, _scaleUpDuration).SetEase(_easeUp))
					.Append(_text.transform.DOScale(Vector3.one, _scaleDownDuration).SetEase(_easeDown))
					.OnComplete(_onComplete);
		}

		public void Construct(IComponentProvider componentProvider) =>
			_experienceHolder = componentProvider.Get<IExperienceHolder>();

		private EventHandler _refresh;
		private IExperienceHolder _experienceHolder;
		private readonly StringBuilder _stringBuilder = new StringBuilder();
		private Sequence _sequence;
		private TweenCallback _onComplete;
	}
}