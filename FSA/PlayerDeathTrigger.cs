using Combat.Characters;
using Combat.Characters.Events;
using Components;
using Plugins.FSA.Components.Easy;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FSA
{
	public sealed class PlayerDeathTrigger : CharacterDeathGlobalEventListener
	{
		[SerializeField, Required, AssetSelector]
		private EasyTrigger _trigger = default;

		protected override void OnEvent(CharacterDeathArgs args)
		{
			if (ReferenceEquals(args.Character, _player))
				_fsa.Trigger(_trigger);
		}

		public void Construct(IComponentProvider provider, EasyFsa fsa)
		{
			_player = provider.Get<Character>();
			_fsa = fsa;
		}

		private Character _player;
		private EasyFsa _fsa;
	}
}