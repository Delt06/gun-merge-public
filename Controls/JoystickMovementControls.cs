using Components;
using Graphics.External_Assets.Joystick_Pack.Scripts;
using Movement;
using UnityEngine;
using UnityUtilities;

namespace Controls
{
	public sealed class JoystickMovementControls : MonoBehaviour
	{
		private void Update()
		{
			var right = _camera.right;
			var forward = Quaternion.AngleAxis(-90f, Vector3.up) * right;
			var (x, y) = InputValue;
			var direction = right * x + forward * y;
			direction.y = 0f;

			if (direction.sqrMagnitude > 1f)
				direction.Normalize();

			_movement.Direction = direction;
		}

		private Vector2 InputValue
		{
			get
			{
				var value = _joystick.Value;
				value.x += Input.GetAxisRaw("Horizontal");
				value.y += Input.GetAxisRaw("Vertical");
				return value;
			}
		}

		private void Awake()
		{
			_camera = Camera.main.transform;
		}

		public void Construct(IComponentProvider componentProvider, Joystick joystick)
		{
			_movement = componentProvider.Get<IMovement>();
			_joystick = joystick;
		}

		private Joystick _joystick;
		private IMovement _movement;
		private Transform _camera;
	}
}