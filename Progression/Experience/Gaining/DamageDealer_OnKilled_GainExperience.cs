using Combat.Characters.Events;
using Combat.Events;
using Progression.Experience.Data;

namespace Progression.Experience.Gaining
{
	public sealed class DamageDealer_OnKilled_GainExperience : DamageDealer_OnKilled_Base
	{
		protected override void OnKilled(CharacterDeathArgs args)
		{
			var experience = _killExperienceProvider.GetExperienceForVictimKilled(args.Level);
			_experienceHolder.Gain(experience);
		}

		public void Construct(IExperienceHolder experienceHolder, IKillExperienceProvider killExperienceProvider)
		{
			_experienceHolder = experienceHolder;
			_killExperienceProvider = killExperienceProvider;
		}

		private IExperienceHolder _experienceHolder;
		private IKillExperienceProvider _killExperienceProvider;
	}
}