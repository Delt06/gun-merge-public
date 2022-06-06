using System;
using Combat;
using Combat.Events;
using Plugins.FSA.Components.Easy;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FSA
{
	public sealed class DeathTrigger : MonoBehaviour
	{
		[SerializeField, Required] private EasyTrigger _trigger = default;

		private void OnEnable()
		{
			_canDie.Died += _onDied;
		}

		private void OnDisable()
		{
			_canDie.Died -= _onDied;
		}

		private void Awake()
		{
			_onDied = (sender, args) => _fsa.Trigger(_trigger);
		}

		public void Construct(ICanDie canDie, EasyFsa fsa)
		{
			_canDie = canDie;
			_fsa = fsa;
		}

		private EventHandler<DeathArgs> _onDied;
		private ICanDie _canDie;
		private EasyFsa _fsa;
	}
}