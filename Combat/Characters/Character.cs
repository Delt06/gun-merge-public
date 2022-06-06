using System;
using Combat.Characters.Events;
using Combat.Events;
using Combat.Teams;
using JetBrains.Annotations;
using UnityEngine;

namespace Combat.Characters
{
	public class Character : MonoBehaviour, IDamageTaker, ICanDie, IHasHealth, IDamageDealer
	{
		[SerializeField, Min(0f)] private float _maxHealth = 100f;

		public void OnDealtDamage(in DamageArgs args) => DealtDamage?.Invoke(this, args);

		public event EventHandler<DamageArgs> DealtDamage;
		public void OnKilled(in CharacterDeathArgs args) => Killed?.Invoke(this, args);

		public event EventHandler<CharacterDeathArgs> Killed;

		public bool IsAlive { get; private set; } = true;

		public ITeam Team { get; private set; }

		bool IHasHealth.IsModifiable => IsAlive;

		public float Health
		{
			get => Mathf.Max(_health, 0f);
			set
			{
				CheckHealthModifiability();
				_health = Mathf.Min(value, MaxHealth);
				HealthChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		private void CheckHealthModifiability()
		{
			if (!IsAlive) throw new InvalidOperationException("Health is not modifiable.");
		}

		public float MaxHealth
		{
			get => _maxHealth;
			set
			{
				CheckHealthModifiability();
				_maxHealth = Mathf.Max(value, 0f);
				MaxHealthChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public event EventHandler HealthChanged;
		public event EventHandler MaxHealthChanged;

		public void Take(float damage, IDamageDealer damageDealer = null)
		{
			if (!IsAlive) return;

			var damageTaken = Mathf.Min(Health, damage);
			Health -= damage;
			OnTookDamage(damageTaken, damageDealer);
			if (Health > 0f) return;

			IsAlive = false;
			OnDied(damageDealer);
		}

		private void OnTookDamage(float damageTaken, IDamageDealer damageDealer)
		{
			var args = new DamageArgs
			{
				Damage = damageTaken,
				DamageDealer = damageDealer
			};

			damageDealer?.OnDealtDamage(args);
			Took?.Invoke(this, args);
		}

		public event EventHandler<DamageArgs> Took;

		private void OnDied([CanBeNull] IDamageDealer damageDealer = null)
		{
			var characterDeathArgs = new CharacterDeathArgs
			{
				Character = this,
				Position = transform.position,
				Rotation = transform.rotation,
				Death = new DeathArgs {Killer = damageDealer}
			};

			foreach (var argsBuilder in _argsBuilders)
			{
				argsBuilder.Build(ref characterDeathArgs);
			}

			damageDealer?.OnKilled(characterDeathArgs);
			CharacterDied?.Invoke(this, characterDeathArgs);
			Died?.Invoke(this, characterDeathArgs.Death);
		}

		public event EventHandler<DeathArgs> Died;
		public event EventHandler<CharacterDeathArgs> CharacterDied;

		private void OnEnable()
		{
			IsAlive = true;
			Health = _maxHealth;
		}

		private void Awake()
		{
			Team = _teamFactory.CreateFor(this);
			_argsBuilders = GetComponentsInChildren<IDeathArgsBuilder>();
		}

		public void Construct(ITeamFactory teamFactory)
		{
			_teamFactory = teamFactory;
		}

		private float _health;
		private ITeamFactory _teamFactory;
		private IDeathArgsBuilder[] _argsBuilders;
	}
}