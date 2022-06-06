using System.Collections.Generic;
using Components;

namespace Combat.Characters
{
	public interface ICharacterRegister : IReadOnlyCharacterRegister
	{
		void Add(IComponentProvider character);
		void Remove(IComponentProvider character);
	}

	public interface IReadOnlyCharacterRegister : IReadOnlyCollection<IComponentProvider> { }
}