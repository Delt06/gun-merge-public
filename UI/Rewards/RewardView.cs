using System.Text;
using Progression.Rewards;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Rewards
{
	public sealed class RewardView : MonoBehaviour
	{
		[SerializeField, Required] private Image _iconImage = default;
		[SerializeField, Required] private TMP_Text _text = default;

		public void Initialize(IReward reward, RewardViewList rewardList)
		{
			_reward = reward;
			_rewardList = rewardList;

			_iconImage.sprite = reward.Icon;
			_iconImage.color = reward.IconColor;

			_stringBuilder.Clear();
			reward.GetDescription(_stringBuilder);
			_text.SetText(_stringBuilder);
		}

		private void OnEnable()
		{
			_button.onClick.AddListener(_onClick);
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(_onClick);
		}

		private void Awake()
		{
			_button = GetComponent<Button>();
			_onClick = () => _rewardList.OnPicked(_reward);
		}

		private UnityAction _onClick;
		private Button _button;
		private RewardViewList _rewardList;
		private IReward _reward;
		private readonly StringBuilder _stringBuilder = new StringBuilder();
	}
}