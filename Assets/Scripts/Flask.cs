using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Flask : MonoBehaviour {
	[SerializeField] private FlaskColor color;

	[Header("Actions")]
	[SerializeField] private SteamVR_Action_Boolean pickupAction;
	
	[Header("Components")]
	[SerializeField] private Renderer liquidRenderer;
	[SerializeField] private Interactable interactable;
	
	private bool isHovering = false;

	private void Awake() {
		Debug.Assert(liquidRenderer != null, "Flask.liquidRenderer is null");
	}

	private void Start() {
		liquidRenderer.material.SetColor(MaterialKeywords.Color, color.ToColor() ?? Color.black);
	}
	
	private void OnHandHoverBegin() {
		isHovering = true;
	}

	private void OnHandHoverEnd() {
		isHovering = false;
	}
}