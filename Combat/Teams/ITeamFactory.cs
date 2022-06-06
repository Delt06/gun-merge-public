using Combat.Characters;

namespace Combat.Teams
{
	public interface ITeamFactory
	{
		ITeam CreateFor(Character character);
	}
}