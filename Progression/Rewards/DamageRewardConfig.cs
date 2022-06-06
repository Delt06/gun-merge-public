using System.Text;
using Combat.Weapons.Coefficients;
using Components;
using UnityEngine;

namespace Progression.Rewards
{
	[CreateAssetMenu(menuName = AssetPath + "Damage Coefficient")]
	public sealed class DamageRewardConfig : RewardConfig
	{
		[SerializeField, Min(0f)] private float _extraCoefficient = 1f;

		public override void GetAcquired(IComponentProvider componentProvider)
		{
			var coefficient = componentProvider.Get<DamageCoefficient>();
			coefficient.Value += _extraCoefficient;
		}

		public override void GetDescription(StringBuilder stringBuilder)
		{
			stringBuilder.Append("Damage +")
				.AppendRatioAsPercentage(_extraCoefficient);
		}
	}
}