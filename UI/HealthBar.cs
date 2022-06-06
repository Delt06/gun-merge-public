using System;
using Combat;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class HealthBar : MonoBehaviour
	{
		private void Start()
		{
			Refresh();
		}

		private void OnEnable()
		{
			Refresh();
			_hasHealth.HealthChanged += _onHealthChanged;
			_hasHealth.MaxHealthChanged += _onHealthChanged;
		}

		private void OnDisable()
		{
			Refresh();
			_hasHealth.HealthChanged -= _onHealthChanged;
			_hasHealth.MaxHealthChanged -= _onHealthChanged;
		}

		private void Awake()
		{
			_onHealthChanged = (sender, args) => Refresh();
		}

		private void Refresh()
		{
			_image.fillAmount = _hasHealth.Health / _hasHealth.MaxHealth;
		}

		public void Construct(IHasHealth hasHealth, Image image)
		{
			_hasHealth = hasHealth;
			_image = image;
		}

		private EventHandler _onHealthChanged;
		private IHasHealth _hasHealth;
		private Image _image;
	}
}