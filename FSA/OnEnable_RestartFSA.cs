using Plugins.FSA.Components.Easy;
using UnityEngine;

namespace FSA
{
	[RequireComponent(typeof(EasyFsa))]
	public class OnEnable_RestartFSA : MonoBehaviour
	{
		private void OnEnable()
		{
			_fsa.Stop();
			_fsa.Run();
		}

		private void Awake()
		{
			_fsa = GetComponent<EasyFsa>();
		}

		private EasyFsa _fsa;
	}
}