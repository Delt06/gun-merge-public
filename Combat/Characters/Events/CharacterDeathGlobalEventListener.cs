using Events;

namespace Combat.Characters.Events
{
	public abstract class
		CharacterDeathGlobalEventListener : GlobalEventListener<CharacterDeathArgs, CharacterDeathGlobalEvent> { }
}