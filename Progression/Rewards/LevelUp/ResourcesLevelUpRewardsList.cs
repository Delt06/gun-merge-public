using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Progression.Rewards.LevelUp
{
	public sealed class ResourcesLevelUpRewardsList : MonoBehaviour, ILevelUpRewardsList
	{
		public IEnumerator<IReward> GetEnumerator() => ((IEnumerable<RewardConfig>) _rewards).GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public int Count => _rewards.Length;

		public IReward this[int index]
		{
			get
			{
				if (index < 0 && index >= Count) throw new ArgumentOutOfRangeException(nameof(index));
				return _rewards[index];
			}
		}

		private void Awake()
		{
			_rewards = Resources.LoadAll<RewardConfig>("");
		}

		private RewardConfig[] _rewards;
	}
}