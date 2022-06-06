using UnityEngine;

namespace Skins
{
	public sealed class RandomSkinSelector : MonoBehaviour
	{
		private void OnEnable()
		{
			if (_skins.Length == 0) return;

			var chosenIndex = Random.Range(0, _skins.Length);
			for (var index = 0; index < _skins.Length; index++)
			{
				_skins[index].gameObject.SetActive(index == chosenIndex);
			}
		}

		private void Awake()
		{
			_skins = GetComponentsInChildren<Skin>(true);
		}

		private Skin[] _skins;
	}
}