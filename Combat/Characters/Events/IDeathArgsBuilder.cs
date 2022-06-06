namespace Combat.Characters.Events
{
	public interface IDeathArgsBuilder
	{
		void Build(ref CharacterDeathArgs args);
	}
}