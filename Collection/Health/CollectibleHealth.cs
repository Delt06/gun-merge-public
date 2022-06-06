using System;
using Combat;
using Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Collection.Health
{
	public sealed class CollectibleHealth : CollectibleBase
	{
		[SerializeField] private HealingMode _healingMode = default;

		[SerializeField, Min(0f), ShowIf(nameof(InAbsoluteMode))]
		private float _restoredHealthPoints = 100f;

		[SerializeField, Range(0f, 1f), ShowIf(nameof(InRelativeMode))]
		private float _restoredHealthRatio = 0.5f;

		protected override void OnGetCollected(IComponentProvider componentProvider)
		{
			var healing = componentProvider.Get<Healing>();
			var restoredHealth = _healingMode switch
			{
				HealingMode.Absolute => _restoredHealthPoints,
				HealingMode.Relative => _restoredHealthRatio * healing.MaxHealth,
				_ => throw new ArgumentOutOfRangeException()
			};
			healing.Heal(restoredHealth);
		}

		private bool InAbsoluteMode => _healingMode == HealingMode.Absolute;
		private bool InRelativeMode => _healingMode == HealingMode.Relative;

		private enum HealingMode
		{
			Absolute,
			Relative
		}
	}
}