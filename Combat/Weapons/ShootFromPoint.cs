using UnityEngine;

namespace Combat.Weapons
{
	public interface IShootFrom
	{
		Vector3 Position { get; }
		Quaternion Rotation { get; }
	}

	public sealed class NullShootFromPoint : IShootFrom
	{
		public Vector3 Position => Vector3.zero;

		public Quaternion Rotation => Quaternion.identity;
	}

	public class ShootFromPoint : MonoBehaviour, IShootFrom
	{
		public Vector3 Position => transform.position;
		public Quaternion Rotation => transform.rotation;
	}
}