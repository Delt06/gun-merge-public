namespace Progression.Experience.Data
{
	public interface IDamageDealingExperienceProvider
	{
		float GetExperienceForDamageDealt(float damageDealt);
	}
}