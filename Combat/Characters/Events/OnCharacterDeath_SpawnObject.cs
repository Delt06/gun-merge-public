using Effects;
using Spawning;

namespace Combat.Characters.Events
{
	public sealed class OnCharacterDeath_SpawnObject : CharacterDeathGlobalEventListener
	{
		protected override void OnEvent(CharacterDeathArgs args)
		{
			_spawner.Spawn(args.Position, args.Rotation);
		}

		public void Construct(ISpawner<Gravestone> spawner) => _spawner = spawner;

		private ISpawner<Gravestone> _spawner;
	}
}