using System;

namespace Progression.Rewards.LevelUp
{
	public interface ILevelUpReward
	{
		int PendingRewardsCount { get; }
		event EventHandler PendingRewardsCountsChanged;
		void GetRandomDistinctRewards(IReward[] rewards);
		void Acquire(IReward reward);
	}
}