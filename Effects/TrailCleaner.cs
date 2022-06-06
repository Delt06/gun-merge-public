using System;
using UnityEngine;

namespace Effects
{
	[RequireComponent(typeof(TrailRenderer))]
	public class TrailCleaner : MonoBehaviour
	{
		private void OnEnable()
		{
			_trailRenderer.Clear();
		}

		private void OnDisable()
		{
			_trailRenderer.Clear();
		}

		private void Awake()
		{
			_trailRenderer = GetComponent<TrailRenderer>();
		}

		private TrailRenderer _trailRenderer;
	}
}