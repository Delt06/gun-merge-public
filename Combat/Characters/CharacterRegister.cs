using System.Collections;
using System.Collections.Generic;
using Components;
using UnityEngine;

namespace Combat.Characters
{
	public class CharacterRegister : MonoBehaviour, ICharacterRegister
	{
		public IEnumerator<IComponentProvider> GetEnumerator() =>
			((IEnumerable<IComponentProvider>) _characters).GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public int Count => _characters.Count;

		public void Add(IComponentProvider character) => _characters.Add(character);

		public void Remove(IComponentProvider character) => _characters.Remove(character);

		private readonly HashSet<IComponentProvider> _characters = new HashSet<IComponentProvider>();
	}
}