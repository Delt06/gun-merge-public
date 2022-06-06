using System.Collections.Generic;
using Combat.Characters;
using UnityEngine;

namespace AI.Movement.Destinations
{
	public class NearestEnemyDestinationProvider : MonoBehaviour, IDestinationProvider
	{
		[SerializeField, Min(0f)] private float _desiredDistance = 10f;
		[SerializeField, Min(0f)] private float _randomOffset = 1f;

		public bool TryFind(out Vector3 destination)
		{
			_otherCharacters.Clear();

			foreach (var provider in _register)
			{
				var otherCharacter = provider.Get<Character>();
				if (!otherCharacter.IsAlive) continue;
				if (!_character.Team.CanAttack(otherCharacter.Team)) continue;

				_otherCharacters.Add(otherCharacter);
			}

			if (_otherCharacters.Count == 0)
			{
				destination = default;
				return false;
			}

			destination = GetClosestDestination();
			var offsetTowardsDestination = destination - _character.transform.position;
			var directionTowardsDestination = offsetTowardsDestination.normalized;
			destination -= directionTowardsDestination * _desiredDistance;
			destination += GetRandomOffset();

			return true;
		}

		private Vector3 GetClosestDestination()
		{
			var currentPosition = _character.transform.position;
			var destination = _otherCharacters[0].transform.position;

			for (var i = 1; i < _otherCharacters.Count; i++)
			{
				var characterPosition = _otherCharacters[i].transform.position;
				var distanceToCharacter = (characterPosition - currentPosition).sqrMagnitude;
				var distanceToNearestCharacter = (destination - currentPosition).sqrMagnitude;
				if (distanceToCharacter >= distanceToNearestCharacter) continue;

				destination = characterPosition;
			}

			return destination;
		}

		private Vector3 GetRandomOffset()
		{
			var distance = Random.Range(0f, _randomOffset);
			var angle = Random.Range(0f, 360f);
			return Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward * distance;
		}

		public void Construct(Character character, IReadOnlyCharacterRegister register)
		{
			_character = character;
			_register = register;
		}

		private readonly List<Character> _otherCharacters = new List<Character>();
		private Character _character;
		private IReadOnlyCharacterRegister _register;
	}
}