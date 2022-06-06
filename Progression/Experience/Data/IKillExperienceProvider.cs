namespace Progression.Experience.Data
{
	public interface IKillExperienceProvider
	{
		float GetExperienceForVictimKilled(int victimLevel);
	}
}