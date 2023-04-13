using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using MessageSystem;
using UnityEngine;
using UnityEngine.Events;

public class DoorLights : MonoBehaviour {
	[SerializeField] private Transform door;
	
	[SerializeField] private Transform lineOffset;
	[SerializeField] private LineRenderer lineRenderer;

	[SerializeField] private GenericDictionary<LightColor, GameObject> lights;

	[SerializeField] private List<LightColor> pattern;


	private Dictionary<LightColor, Material> lightMaterials = new Dictionary<LightColor, Material>();

	[Header("Events")]
	[SerializeField] private UnityEvent onPatternSuccessEvent;
	[SerializeField] private UnityEvent onPatternFailEvent;
	
	private int currentPatternIndex = 0;

	private void Awake() {
		this.lineRenderer ??= GetComponent<LineRenderer>();

		this.onPatternSuccessEvent ??= new UnityEvent();
		this.onPatternFailEvent ??= new UnityEvent();

		foreach (KeyValuePair<LightColor, GameObject> pair in lights) {
			// cache material components
			LightColor lightColor = pair.Key;
			GameObject lightGameObject = pair.Value;

			Material material = lightGameObject.GetComponent<Renderer>().material;
			lightMaterials[lightColor] = material;

			// set color and disable emission
			material.color = lightColor.ToColor() ?? Color.black;
			
			material.SetColor(MaterialKeywords.EmissionColor, lightColor.ToColor() ?? Color.black);
			material.DisableKeyword(MaterialKeywords.Emission);
		}
	}
	

	private void Start() {
		this.pattern = GetRandomPattern();
		UpdateLineRenderer(this.pattern);

		// lineRenderer.enabled = true;
	}

	private void OnValidate() {
		UpdateLineRenderer(pattern);
	}

	public void ActivateLights() {
		Debug.Log("Activating lights");
		
		// reset progress
		currentPatternIndex = 0;
		
		PulsateLight(pattern[currentPatternIndex]);
		
		MessageManager.instance.Listen(OnMessage, MessageTopics.ControlPanelButtons);
	}

	public void DeactivateLights() {
		Debug.Log("Deactivating lights");
		
		MessageManager.instance.Unlisten(OnMessage);
	}

	private void PulsateLight(LightColor lightColor) {
		Material material = lightMaterials[lightColor];

		Color newColor = Color.Lerp(Color.white, lightColor.ToColor() ?? Color.black, 0.5f);
		
		material.EnableKeyword(MaterialKeywords.Emission);
		material
			.DOColor(newColor, MaterialKeywords.EmissionColor, 1f)
			.SetLoops(-1, LoopType.Yoyo);
	}

	private void OnValidButtonPress() {
		{ // stop pulsating on current light
			LightColor currentColor = pattern[currentPatternIndex];
			Material currentMaterial = lightMaterials[currentColor];

			// skip to end of animation to stay on high emission
			currentMaterial.DOGoto(1.0f);
		}

		currentPatternIndex += 1;
		if (currentPatternIndex >= pattern.Count) {
			DeactivateLights();
			onPatternSuccessEvent?.Invoke();
			return;
		}

		{
			LightColor newColor = pattern[currentPatternIndex];
			PulsateLight(newColor);
		}
	}

	public void RandomizePattern() {
		this.pattern = GetRandomPattern();
		UpdateLineRenderer(this.pattern);

		currentPatternIndex = 0;
		
		// TODO handle tweens

		foreach (Material material in lightMaterials.Values) {
			material.DisableKeyword(MaterialKeywords.Emission);
			material.DOKill();
		}
	}

	private void OnMessage(Message message) {
		LightColor currentColor = pattern[currentPatternIndex];

		string pressedButton = message.payload.First(pair => (bool)pair.Value == true).Key;
		
		if (pressedButton == currentColor.ToString().ToLower()) {
			OnValidButtonPress();
		}
		else {
			onPatternFailEvent?.Invoke();
		}
	}
	
	private List<LightColor> GetRandomPattern() {
		List<LightColor> pattern = new List<LightColor>();
		
		// add all colors to the pattern
		foreach (LightColor color in Enum.GetValues(typeof(LightColor))) {
			pattern.Add(color);
		}
		
		pattern.Shuffle();

		return pattern;
	}

	private void UpdateLineRenderer(List<LightColor> pattern) {
		lineRenderer.positionCount = pattern.Count;
		
		for (int i = 0; i < pattern.Count; i++) {
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
	}

	private void OnDisable() {
		DeactivateLights();
	}
}