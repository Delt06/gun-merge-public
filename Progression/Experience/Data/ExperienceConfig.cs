using UnityEngine;

namespace Progression.Experience.Data
{
	[CreateAssetMenu(menuName = "Progression/Experience Config")]
	public sealed class ExperienceConfig : ScriptableObject, ILevelRequirementProvider, IKillExperienceProvider,
		IDamageDealingExperienceProvider
	{
		[SerializeField]
		private AnimationCurve _levelExperienceRequirements = AnimationCurve.Linear(2f, 100f, 100f, 1000f);

		[SerializeField, Min(0f)] private float _levelOfVictimToExperience = 100f;
		[SerializeField, Min(0f)] private float _damageDealtToExperience = 1f;

		public float GetRequirementForLevel(int level) => _levelExperienceRequirements.Evaluate(level);
		public float GetExperienceForVictimKilled(int victimLevel) => victimLevel * _levelOfVictimToExperience;
		public float GetExperienceForDamageDealt(float damageDealt) => damageDealt * _damageDealtToExperience;
	}
}