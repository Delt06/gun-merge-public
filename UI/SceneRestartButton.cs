using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.SceneManagement.SceneManager;

namespace UI
{
	public class SceneRestartButton : MonoBehaviour, IPointerClickHandler
	{
		public void OnPointerClick(PointerEventData eventData)
		{
			LoadScene(GetActiveScene().buildIndex);
		}
	}
}