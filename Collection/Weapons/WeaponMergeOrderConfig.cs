using System;
using System.Collections;
using System.Collections.Generic;
using Combat.Weapons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Collection.Weapons
{
	[CreateAssetMenu]
	public sealed class WeaponMergeOrderConfig : ScriptableObject, IWeaponMergeOrder
	{
		[SerializeField, Required] private WeaponConfig[] _order = default;

		public bool InOrder(WeaponConfig weapon, out int index)
		{
			index = Array.IndexOf(_order, weapon);
			return index != -1;
		}

		public int Compare(WeaponConfig weapon1, WeaponConfig weapon2)
		{
			if (!InOrder(weapon1, out var index1)) return 0;
			if (!InOrder(weapon2, out var index2)) return 0;

			return index1 - index2;
		}

		public IEnumerator<WeaponConfig> GetEnumerator() => ((IEnumerable<WeaponConfig>) _order).GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public int Count => _order.Length;

		public WeaponConfig this[int index]
		{
			get
			{
				if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException(nameof(index));
				return _order[index];
			}
		}
	}
}