using System;
using Components;
using Progression.Experience;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public sealed class ExperienceBar : MonoBehaviour
	{
		[SerializeField, Required] private Image _image = default;
		[SerializeField, Min(0f)] private float _speed = 1f;

		private void Update()
		{
			var currentFillAmount = _image.fillAmount;

			if (_targetFillAmount != null && !Mathf.Approximately(currentFillAmount, _targetFillAmount.Value))
			{
				var actualTarget = currentFillAmount < _targetFillAmount ? _targetFillAmount.Value : 1f;
				currentFillAmount = Mathf.MoveTowards(currentFillAmount, actualTarget, _speed * Time.deltaTime);

				if (currentFillAmount >= _targetFillAmount && Mathf.Approximately(currentFillAmount, 1f))
					currentFillAmount = 0f;

				_image.fillAmount = currentFillAmount;
			}
		}

		private void Start()
		{
			Refresh(true);
		}

		private void OnEnable()
		{
			Refresh(true);
			_experienceHolder.ExperienceChanged += _refresh;
		}

		private void OnDisable()
		{
			_experienceHolder.ExperienceChanged -= _refresh;
		}

		private void Awake()
		{
			_refresh = (sender, args) => Refresh();
		}

		private void Refresh(bool force = false)
		{
			var experience = _experienceHolder.Experience;
			var requiredExperience = _experienceHolder.GetRequirementForNextLevel();
			var targetRatio = experience / requiredExperience;
			_targetFillAmount = targetRatio;

			if (force)
				_image.fillAmount = targetRatio;
		}

		public void Construct(IComponentProvider componentProvider)
		{
			_experienceHolder = componentProvider.Get<IExperienceHolder>();
		}

		private EventHandler _refresh;
		private IExperienceHolder _experienceHolder;
		private float? _targetFillAmount;
	}
}