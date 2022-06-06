using Combat.Events;
using Combat.Weapons;
using JetBrains.Annotations;
using UnityEngine;

namespace Combat.Characters.Events
{
	public struct CharacterDeathArgs
	{
		public Character Character;
		public Vector3 Position;
		public Quaternion Rotation;
		public WeaponConfig WeaponConfig;
		public int Level;

		public DeathArgs Death;
	}
}