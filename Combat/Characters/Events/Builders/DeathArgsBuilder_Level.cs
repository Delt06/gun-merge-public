using Progression;
using Progression.Experience;
using UnityEngine;

namespace Combat.Characters.Events.Builders
{
	public sealed class DeathArgsBuilder_Level : MonoBehaviour, IDeathArgsBuilder
	{
		public void Build(ref CharacterDeathArgs args) => args.Level = _experienceHolder.Level;

		public void Construct(IExperienceHolder experienceHolder)
		{
			_experienceHolder = experienceHolder;
		}

		private IExperienceHolder _experienceHolder;
	}
}