using System.Text;
using AssetIcons;
using Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Progression.Rewards
{
	public abstract class RewardConfig : ScriptableObject, IReward
	{
		[Required, AssetIcon] public Sprite Icon = default;

		[SerializeField] private Color _iconColor = Color.white;

		public Color IconColor => _iconColor;

		public abstract void GetAcquired(IComponentProvider componentProvider);

		Sprite IReward.Icon => Icon;

		public abstract void GetDescription(StringBuilder stringBuilder);

		protected const string AssetPath = "Progression/Rewards/";
	}
}