using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mixer : MonoBehaviour {

	[SerializeField] private GenericDictionary<FlaskColor, bool> solution;
	
	[Header("Events")]
	[SerializeField] private UnityEvent onCorrectSolutionEvent;

	[Header("Components")]
	[SerializeField] private GenericDictionary<FlaskColor, GameObject> stands;

	private Dictionary<FlaskColor, Material> standMaterials;

	private Dictionary<FlaskColor, bool> buttonStates;

	public void ToggleButtonStateFromString(string name) {
		FlaskColor flaskColor = (FlaskColor) Enum.Parse(typeof(FlaskColor), name);
		ToggleButtonState(flaskColor);
	}

	public void ToggleButtonState(FlaskColor flaskColor) {
		Debug.Log("Toggling button state for " + flaskColor + "");
		bool newButtonState = !buttonStates[flaskColor];

		buttonStates[flaskColor] = newButtonState;
		
		// update stand material
		Material material = standMaterials[flaskColor];
		
		Color newColor = newButtonState ? flaskColor.ToColor() ?? Color.black : Color.white;
		
		material.SetColor(MaterialKeywords.EmissionColor, newColor);
		
		if (newButtonState) {
			material.EnableKeyword(MaterialKeywords.Emission);
		} else {
			material.DisableKeyword(MaterialKeywords.Emission);
		}

		CheckSolutionMatch();
	}

	private void Awake() {
		onCorrectSolutionEvent ??= new UnityEvent();
		
		stands ??= new GenericDictionary<FlaskColor, GameObject>();
		// buttons ??= new GenericDictionary<FlaskColor, GameObject>();
		
		buttonStates = new Dictionary<FlaskColor, bool>();
		
		// cache materials
		standMaterials = new Dictionary<FlaskColor, Material>();
		
		foreach (KeyValuePair<FlaskColor, GameObject> pair in stands) {
			FlaskColor flaskColor = pair.Key;
			GameObject stand = pair.Value;
			
			Material material = stand.GetComponent<Renderer>().material;
			standMaterials[flaskColor] = material;
		}
	}
	
	private void Start() {
		// reset button state
		foreach (KeyValuePair<FlaskColor, bool> pair in solution) {
			FlaskColor flaskColor = pair.Key;
			buttonStates[flaskColor] = false;
		}
		
		// // set color of stands
		// foreach (KeyValuePair<FlaskColor, GameObject> pair in stands) {
		// 	FlaskColor flaskColor = pair.Key;
		// 	GameObject stand = pair.Value;
		// 	
		// 	Renderer standRenderer = stand.GetComponent<Renderer>();
		// 	standRenderer.material.color = flaskColor.ToColor() ?? Color.black;
		// }
	}

	private void CheckSolutionMatch() {
		bool isCorrect = true;

		foreach (KeyValuePair<FlaskColor, bool> pair in buttonStates) {
			FlaskColor flaskColor = pair.Key;
			bool buttonState = pair.Value;

			if (buttonState != solution[flaskColor]) {
				isCorrect = false;
				break;
			}
		}

		Debug.Log($"Solution correct? {isCorrect}");

		if (isCorrect == false) {
			return;
		}
		
		onCorrectSolutionEvent?.Invoke();
	}
}
