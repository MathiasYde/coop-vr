using System;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class AdminCommunicator : MonoBehaviour {

	private Vector2Int previousPosition = Vector2Int.zero;
	private void Awake() {
		
	}

	private void Update() {
		Vector2Int position = Player.instance.transform.position.xz().ToVector2Int();
		Debug.Log(position);
	}
}
