using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Combat.Characters.Events
{
	public sealed class OnLocalCharacterDeath_PropagateToGlobalDeathEvent : MonoBehaviour
	{
		[SerializeField, Required, AssetSelector]
		private CharacterDeathGlobalEvent _event = default;

		private void OnEnable()
		{
			_character.CharacterDied += _onDied;
		}

		private void OnDisable()
		{
			_character.CharacterDied -= _onDied;
		}

		private void Awake()
		{
			_onDied = (sender, args) => _event.Raise(args);
		}

		public void Construct(Character character)
		{
			_character = character;
		}

		private EventHandler<CharacterDeathArgs> _onDied;
		private Character _character;
	}
}