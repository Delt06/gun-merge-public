using Combat.Events;
using Progression.Experience.Data;

namespace Progression.Experience.Gaining
{
	public sealed class DamageDealer_OnDealtDamage_GainExperience : DamageDealer_OnDealtDamage_Base
	{
		protected override void OnKilled(DamageArgs args)
		{
			var experience = _damageDealingExperienceProvider.GetExperienceForDamageDealt(args.Damage);
			_experienceHolder.Gain(experience);
		}

		public void Construct(IExperienceHolder experienceHolder,
			IDamageDealingExperienceProvider damageDealingExperienceProvider)
		{
			_experienceHolder = experienceHolder;
			_damageDealingExperienceProvider = damageDealingExperienceProvider;
		}

		private IExperienceHolder _experienceHolder;
		private IDamageDealingExperienceProvider _damageDealingExperienceProvider;
	}
}