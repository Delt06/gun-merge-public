using System;
using System.Text;
using Combat;
using TMPro;
using UnityEngine;

namespace UI
{
	public sealed class HealthText : MonoBehaviour
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
			_stringBuilder.Clear()
				.Append(RoundToInt(_hasHealth.Health))
				.Append("/")
				.Append(RoundToInt(_hasHealth.MaxHealth));
			_text.SetText(_stringBuilder);
		}

		private static int RoundToInt(float value) => Mathf.CeilToInt(value);

		public void Construct(IHasHealth hasHealth, TMP_Text text)
		{
			_hasHealth = hasHealth;
			_text = text;
		}

		private EventHandler _onHealthChanged;
		private IHasHealth _hasHealth;
		private TMP_Text _text;
		private readonly StringBuilder _stringBuilder = new StringBuilder();
	}
}