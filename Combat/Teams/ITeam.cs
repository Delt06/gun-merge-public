using JetBrains.Annotations;

namespace Combat.Teams
{
	public interface ITeam
	{
		bool CanAttack([CanBeNull] ITeam otherTeam);
	}
}