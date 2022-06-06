using Combat.Weapons;
using Components;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Controls
{
	public sealed class ShootingButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
	{
		[SerializeField] private string _fallbackButton = "Fire1";

		public void OnPointerDown(PointerEventData eventData)
		{
			_pointerId ??= eventData.pointerId;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (eventData.pointerId != _pointerId) return;
			_pointerId = null;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (eventData.pointerId != _pointerId) return;
			_pointerId = null;
		}

		private void Update()
		{
			if (_pointerId != null || Input.GetButton(_fallbackButton))
				_weapon.TryUse();
		}

		private void OnDisable()
		{
			_pointerId = null;
		}

		public void Construct(IComponentProvider provider) => _weapon = provider.Get<IWeapon>();

		private IWeapon _weapon;
		private int? _pointerId;
	}
}