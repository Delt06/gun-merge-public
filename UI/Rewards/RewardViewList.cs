using System;
using Progression.Rewards;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI.Rewards
{
	public sealed class RewardViewList : MonoBehaviour
	{
		[SerializeField, Required] private RewardView[] _rewardViews = default;

		public IReward[] Rewards => _rewards ??= new IReward[_rewardViews.Length];

		public bool IsShown
		{
			get => gameObject.activeSelf;
			private set => gameObject.SetActive(value);
		}

		public void Initialize()
		{
			for (var index = 0; index < _rewardViews.Length; index++)
			{
				var reward = Rewards[index];
				var rewardView = _rewardViews[index];
				rewardView.Initialize(reward, this);
			}

			IsShown = true;
		}

		public void OnPicked(IReward reward)
		{
			IsShown = false;
			Picked?.Invoke(this, reward);
		}

		public event EventHandler<IReward> Picked;

		private IReward[] _rewards;
	}
}