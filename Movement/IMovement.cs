using UnityEngine;

namespace Movement
{
	public interface IMovement
	{
		float Speed { get; }
		float AngularSpeed { get; }
		Vector3 CurrentPosition { get; }
		Vector3 Direction { get; set; }
		Quaternion? RotationOverride { get; set; }
		bool IsMoving { get; }
		Transform Transform { get; }
	}
}