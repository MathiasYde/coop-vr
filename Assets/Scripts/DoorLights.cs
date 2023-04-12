using System;
using System.Collections.Generic;
using MessageSystem;
using UnityEngine;

public class DoorLights : MonoBehaviour {
	[SerializeField] private Transform lineOffset;
	[SerializeField] private LineRenderer lineRenderer;

	[SerializeField] private GenericDictionary<LightColor, GameObject> lights;

	[SerializeField] private List<LightColor> pattern;

	private void Awake() {
		lineRenderer ??= GetComponent<LineRenderer>();
	}

	private void Start() {
		RandomizePattern();
		UpdateLineRenderer(pattern);
		SetLightColors();

		lineRenderer.enabled = true;
	}
	
	private void SetLightColors() {
		foreach (KeyValuePair<LightColor, GameObject> pair in lights) {
			Material material = pair.Value.GetComponent<Renderer>().material;
			material.color = pair.Key.ToColor() ?? Color.black;
		}
	}

	private void OnValidate() {
		UpdateLineRenderer(pattern);
	}

	public void ActivateLights() {
		Debug.Log("Activating lights");
		// MessageManager.Listen(OnMessage, "door_open");
		MessageManager.instance.Listen(OnMessage, "controlpanel/buttons");
	}

	public void DeactivateLights() {
		Debug.Log("Deactivating lights");
		
		MessageManager.instance.Unlisten(OnMessage);
	}

	private void OnMessage(Message message) {
		Debug.Log("Got message");
		Debug.Log(message);
	}
	
	private void RandomizePattern() {
		pattern.Clear();
		
		// add all colors to the pattern
		foreach (LightColor color in Enum.GetValues(typeof(LightColor))) {
			pattern.Add(color);
		}
		
		pattern.Shuffle();
	}

	private void UpdateLineRenderer(List<LightColor> lightOrder) {
		lineRenderer.positionCount = lightOrder.Count;
		
		for (int i = 0; i < lightOrder.Count; i++) {
			LightColor color = pattern[i];
			GameObject gameObject = lights[color];
			
			lineRenderer.SetPosition(i, gameObject.transform.localPosition + lineOffset.localPosition);
		}
	}

	private void OnDrawGizmos() {
		// draw the pattern
		Vector3 offset = transform.TransformVector(lineOffset.localPosition);

		List<Vector3> positions = new List<Vector3>();
		
		for (int i = 0; i < pattern.Count; i++) {
			LightColor color = pattern[i];
			GameObject gameObject = lights[color];
			
			positions.Add(gameObject.transform.position + offset);
		}
		
		Gizmos.color = Color.red;
		
		for (int i = 0; i < pattern.Count - 1; i++) {
			Vector3 current = positions[i];
			Vector3 next = positions[i + 1];
			
			Gizmos.DrawLine(current, next);
		}

		// List<Vector3> positions = new List<Vector3>();
		// foreach (GameObject gameObject in lights.Values) {
		// 	positions.Add(gameObject.transform.position + offset);
		// }
		//
		// Gizmos.color = Color.red;
		//
		// for (int i = 0; i < pattern.Count - 1; i++) {
		// 	Vector3 current = positions[i];
		// 	Vector3 next = positions[i + 1];
		// 	
		// 	Gizmos.DrawLine(current, next);
		// }
	}

	private void OnDisable() {
		DeactivateLights();
	}
}
