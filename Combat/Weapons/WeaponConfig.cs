using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Combat.Weapons
{
	[CreateAssetMenu(menuName = "Weapon/Basic")]
	public class WeaponConfig : ScriptableObject
	{
		[SerializeField, Min(0), LabelText("ID")]
		private int _id = 0;

		[SerializeField, Min(0f)] private float _bulletDamage = 100f;
		[SerializeField, Min(0f)] private float _shotsPerSecond = 10f;
		[SerializeField, Min(1)] private int _bulletsPerShot = 1;
		[SerializeField, Min(0f)] private float _maxRecoilAngle = 5f;

		public float BulletDamage => _bulletDamage;

		public float ShotsPerSecond => _shotsPerSecond;

		public int BulletsPerShot => _bulletsPerShot;

		public float MaxRecoilAngle => _maxRecoilAngle;

		public int ID => _id;

		private void OnValidate()
		{
			var allWeapons = Resources.LoadAll<WeaponConfig>("");
			if (allWeapons.Count(w => w.ID == _id) > 1)
				_id = allWeapons.Select(w => w.ID).Max() + 1;
		}
	}
}