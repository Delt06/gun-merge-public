using UnityEngine;

namespace AI.Movement.Destinations
{
	public interface IDestinationProvider
	{
		bool TryFind(out Vector3 destination);
	}
}