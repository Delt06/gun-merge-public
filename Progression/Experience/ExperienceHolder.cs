using System;
using Progression.Experience.Data;
using UnityEngine;

namespace Progression.Experience
{
	public sealed class ExperienceHolder : MonoBehaviour, IExperienceHolder
	{
		public void Gain(float experience)
		{
			Experience += experience;

			var requirement = GetRequirementForNextLevel();
			while (Experience >= requirement)
			{
				Experience -= requirement;
				Level++;
				requirement = GetRequirementForNextLevel();
			}
		}

		public float GetRequirementForNextLevel() => _levelRequirementProvider.GetRequirementForLevel(Level + 1);

		public float Experience
		{
			get => _experience;
			private set
			{
				_experience = value;
				ExperienceChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public event EventHandler ExperienceChanged;

		public int Level
		{
			get => _level;
			private set
			{
				_level = value;
				LevelChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public event EventHandler LevelChanged;

		private void OnEnable()
		{
			Experience = 0f;
			Level = 1;
		}

		public void Construct(ILevelRequirementProvider levelRequirementProvider) =>
			_levelRequirementProvider = levelRequirementProvider;

		private float _experience;
		private int _level = 1;
		private ILevelRequirementProvider _levelRequirementProvider;
	}
}