using Components;
using UnityEngine;

namespace Combat.Characters
{
	public class CharacterRegistration : MonoBehaviour
	{
		private void OnEnable()
		{
			_characterRegister.Add(_character);
		}

		private void OnDisable()
		{
			_characterRegister.Remove(_character);
		}

		public void Construct(IComponentProvider character, ICharacterRegister characterRegister)
		{
			_character = character;
			_characterRegister = characterRegister;
		}

		private IComponentProvider _character;
		private ICharacterRegister _characterRegister;
	}
}