using Combat.Characters;

namespace Combat.Teams
{
	public sealed class RivalToAllTeamFactory : ITeamFactory
	{
		public ITeam CreateFor(Character character) => new RivalToAllTeam();
	}
}