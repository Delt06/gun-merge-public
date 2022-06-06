using System;
using Progression.Rewards;
using Progression.Rewards.LevelUp;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI.Rewards
{
	public sealed class LevelUpRewardViewList : MonoBehaviour
	{
		[SerializeField, Required] private RewardViewList _rewardViewList = default;

		private void OnEnable()
		{
			TryShow();
			_levelUpRewards.PendingRewardsCountsChanged += _onPendingCountChanged;
			_rewardViewList.Picked += _onPicked;
		}

		private void OnDisable()
		{
			_levelUpRewards.PendingRewardsCountsChanged -= _onPendingCountChanged;
			_rewardViewList.Picked -= _onPicked;
		}

		private void Awake()
		{
			_onPendingCountChanged = (sender, args) => TryShow();
			_onPicked = (sender, reward) =>
			{
				_levelUpRewards.Acquire(reward);
				TryShow();
			};
		}

		private void TryShow()
		{
			if (_levelUpRewards.PendingRewardsCount == 0) return;
			if (_rewardViewList.IsShown) return;

			_levelUpRewards.GetRandomDistinctRewards(_rewardViewList.Rewards);
			_rewardViewList.Initialize();
		}

		public void Construct(ILevelUpReward levelUpRewards) => _levelUpRewards = levelUpRewards;

		private EventHandler _onPendingCountChanged;
		private ILevelUpReward _levelUpRewards;
		private EventHandler<IReward> _onPicked;
	}
}