using System;
using UnityEngine;

public class Flask : MonoBehaviour {
	[SerializeField] private FlaskColor color;
	
	[Header("Components")]
	[SerializeField] private Renderer liquidRenderer;

	private void Awake() {
		Debug.Assert(liquidRenderer != null, "Flask.liquidRenderer is null");
	}

	private void Start() {
		liquidRenderer.material.SetColor(MaterialKeywords.Color, color.ToColor() ?? Color.black);
	}
}