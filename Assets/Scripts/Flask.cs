using System;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Flask : MonoBehaviour {
	[SerializeField] private FlaskColor color;

	[Header("Actions")]
	[SerializeField] private SteamVR_Action_Boolean pickupAction;
	
	[Header("Events")]
	[SerializeField] private UnityEvent onPickupEvent;
	
	[Header("Components")]
	[SerializeField] private Renderer liquidRenderer;
	[SerializeField] private Interactable interactable;
	
	private bool isHovering = false;

	private void Awake() {
		Debug.Assert(liquidRenderer != null, "Flask.liquidRenderer is null");
		
		onPickupEvent ??= new UnityEvent();
	}

	private void Start() {
		liquidRenderer.material.SetColor(MaterialKeywords.Color, color.ToColor() ?? Color.black);
	}
	
	private void Pickup(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
		if (isHovering) {
			Debug.Log("Picking up flask with color " + color);
			onPickupEvent.Invoke();
		}
	}
	
	private void OnHandHoverBegin() {
		isHovering = true;
	}

	private void OnHandHoverEnd() {
		isHovering = false;
	}
	
	private void OnEnable() {
		pickupAction.onStateDown += Pickup;
	}

	private void OnDisable() {
		pickupAction.onStateDown -= Pickup;
	}
}