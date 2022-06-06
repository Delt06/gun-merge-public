using System;
using Combat.Weapons;
using UnityEngine;

namespace Animations
{
	public sealed class WeaponAnimations : MonoBehaviour
	{
		private void Update()
		{
			_animator.SetFloat(WeaponID, _weapon.Config.ID);
		}

		private void OnEnable()
		{
			_weapon.Used += _onUsed;
		}

		private void OnDisable()
		{
			_weapon.Used -= _onUsed;
		}

		private void Awake()
		{
			_onUsed = (sender, args) => _animator.SetTrigger(ShotId);
		}

		public void Construct(Animator animator, IWeapon weapon)
		{
			_animator = animator;
			_weapon = weapon;
		}

		private Animator _animator;
		private IWeapon _weapon;
		private EventHandler _onUsed;
		private static readonly int ShotId = Animator.StringToHash("Shot");
		private static readonly int WeaponID = Animator.StringToHash("WeaponID");
	}
}