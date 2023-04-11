using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class DoorLights : MonoBehaviour {
	[SerializeField] private Transform lineOffset;
	[SerializeField] private LineRenderer lineRenderer;

	[SerializeField] private GenericDictionary<LightColor, GameObject> lights;

	[SerializeField] private List<int> patteren;

	private void Awake() {
		lineRenderer ??= GetComponent<LineRenderer>();
	}

	private void Start() {
		RandomizePattern();
		UpdateLineRenderer(patteren, lights.Values.ToList());
		SetLightColors();

		lineRenderer.enabled = true;
	}
	
	private void SetLightColors() {
		foreach (KeyValuePair<LightColor, GameObject> pair in lights) {
			// light.Value.GetComponent<Light>().SetColor(light.Key);
			Material material = pair.Value.GetComponent<Renderer>().material;
			material.color = pair.Key.ToColor() ?? Color.black;
			
		}
	}
	
	private void RandomizePattern() {
		patteren = Enumerable.Range(0, lights.Count).ToList();
		patteren.Shuffle();
	}

	private void UpdateLineRenderer(List<int> lightOrder, List<GameObject> lights) {
		lineRenderer.positionCount = lightOrder.Count;
		
		for (int i = 0; i < lightOrder.Count; i++) {
			lineRenderer.SetPosition(i, lights[lightOrder[i]].transform.localPosition + lineOffset.localPosition);
		}
	}

	private void OnDrawGizmos() {
		// draw the pattern
		Vector3 offset = transform.TransformVector(lineOffset.localPosition);
		
		List<Vector3> positions = new List<Vector3>();
		foreach (GameObject gameObject in lights.Values) {
			positions.Add(gameObject.transform.position + offset);
		}
		
		Gizmos.color = Color.red;

		for (int i = 0; i < patteren.Count - 1; i++) {
			Gizmos.DrawLine(positions[patteren[i]], positions[patteren[i + 1]]);
		}
	}
}
