using System.Text;
using Components;
using UnityEngine;

namespace Progression.Rewards
{
	public interface IReward
	{
		void GetAcquired(IComponentProvider componentProvider);
		Sprite Icon { get; }
		Color IconColor { get; }
		void GetDescription(StringBuilder stringBuilder);
	}
}