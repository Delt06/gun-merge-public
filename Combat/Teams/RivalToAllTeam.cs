namespace Combat.Teams
{
	public sealed class RivalToAllTeam : ITeam
	{
		public bool CanAttack(ITeam otherTeam) => !ReferenceEquals(this, otherTeam);
	}
}