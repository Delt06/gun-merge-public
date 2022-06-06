using System;
using System.Collections.Generic;
using Components;
using Progression.Experience;
using UnityEngine;

namespace Progression.Rewards.LevelUp
{
	public sealed class RandomLevelUpReward : MonoBehaviour, ILevelUpReward
	{
		public int PendingRewardsCount
		{
			get => _pendingRewardsCount;
			private set
			{
				_pendingRewardsCount = Mathf.Max(0, value);
				PendingRewardsCountsChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public event EventHandler PendingRewardsCountsChanged;

		public void GetRandomDistinctRewards(IReward[] rewards)
		{
			_rewardBuffer.Clear();
			_rewardBuffer.AddRange(_rewardsList);
			_rewardBuffer.Sort(_randomRewardComparer);

			for (var index = 0; index < rewards.Length; index++)
			{
				var indexInBuffer = index % _rewardBuffer.Count;
				rewards[index] = _rewardBuffer[indexInBuffer];
			}
		}

		public void Acquire(IReward reward)
		{
			if (PendingRewardsCount <= 0) throw new InvalidOperationException("No pending rewards.");
			reward.GetAcquired(_componentProvider);
			PendingRewardsCount--;
		}

		private void Start()
		{
			_experienceHolder = _componentProvider.Get<IExperienceHolder>();
			_previousLevel = _experienceHolder.Level;
		}

		private void OnEnable()
		{
			_experienceHolder.LevelChanged += _onLevelChanged;
		}

		private void OnDisable()
		{
			_experienceHolder.LevelChanged -= _onLevelChanged;
		}

		private void Awake()
		{
			_experienceHolder = _componentProvider.Get<IExperienceHolder>();

			_onLevelChanged = (sender, args) =>
			{
				if (_previousLevel == null)
				{
					_previousLevel = _experienceHolder.Level;
					return;
				}

				if (_experienceHolder.Level <= _previousLevel) return;

				var extraLevels = _experienceHolder.Level - _previousLevel.Value;
				PendingRewardsCount += extraLevels;
				_previousLevel = _experienceHolder.Level;
			};

			_randomRewardComparer = (r1, r2) => _random.Next() % 2 == 0 ? 1 : -1;
		}

		public void Construct(IComponentProvider componentProvider, ILevelUpRewardsList rewardsList)
		{
			_componentProvider = componentProvider;
			_rewardsList = rewardsList;
		}

		private int? _previousLevel;
		private int _pendingRewardsCount;
		private EventHandler _onLevelChanged;
		private IComponentProvider _componentProvider;
		private IExperienceHolder _experienceHolder;
		private ILevelUpRewardsList _rewardsList;
		private readonly List<IReward> _rewardBuffer = new List<IReward>();
		private Comparison<IReward> _randomRewardComparer;
		private readonly System.Random _random = new System.Random();
	}
}