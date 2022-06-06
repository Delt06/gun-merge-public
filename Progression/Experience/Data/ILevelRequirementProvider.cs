namespace Progression.Experience.Data
{
	public interface ILevelRequirementProvider
	{
		float GetRequirementForLevel(int level);
	}
}