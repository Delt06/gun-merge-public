using UnityEngine;

namespace UI
{
	public sealed class LookTowardsCamera : MonoBehaviour
	{
		private void Update()
		{
			transform.forward = -_camera.forward;
		}

		private void Awake()
		{
			_camera = Camera.main.transform;
		}

		private Transform _camera;
	}
}