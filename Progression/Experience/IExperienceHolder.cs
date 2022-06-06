using System;

namespace Progression.Experience
{
	public interface IExperienceHolder
	{
		void Gain(float experience);
		float Experience { get; }
		event EventHandler ExperienceChanged;

		int Level { get; }
		event EventHandler LevelChanged;
		float GetRequirementForNextLevel();
	}
}