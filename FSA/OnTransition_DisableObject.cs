using Plugins.FSA;
using Plugins.FSA.Components.Easy;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FSA
{
	public sealed class OnTransition_DisableObject : EasyTransitionListener
	{
		[SerializeField, Required] private GameObject _gameObject = default;

		protected override void TransitionAction(IFsa<EasyTrigger> fsa, IState<EasyTrigger> oldState,
			IState<EasyTrigger> newState)
		{
			_gameObject.SetActive(false);
		}
	}
}