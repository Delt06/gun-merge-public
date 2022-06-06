using DELTation.UI.Screens;
using Plugins.FSA;
using Plugins.FSA.Components.Easy;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FSA
{
	public sealed class StateObjectGameScreen : MonoBehaviour, ITransitionListener<EasyTrigger>
	{
		[SerializeField, Required] private GameScreen _gameScreen = default;
		[SerializeField, Required] private EasyState _state = default;

		public void OnTransition(IFsa<EasyTrigger> fsa, IState<EasyTrigger> oldState, IState<EasyTrigger> newState)
		{
			var from = ReferenceEquals(oldState, _state);
			var to = ReferenceEquals(newState, _state);

			if (to && !from)
				_gameScreen.Open();
			else if (!to && from)
				_gameScreen.Close();
		}
	}
}