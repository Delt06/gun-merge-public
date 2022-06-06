using UnityEngine;

namespace Animations
{
	public sealed class ConstantAngularVelocity : MonoBehaviour
	{
		[SerializeField] private Vector3 _angularVelocity = Vector3.zero;

		private void Update()
		{
			var deltaAngles = _angularVelocity * Time.deltaTime;
			transform.Rotate(deltaAngles, Space.Self);
		}
	}
}