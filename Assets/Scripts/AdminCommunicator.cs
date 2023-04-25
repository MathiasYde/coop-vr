using System.Collections.Generic;
using MessageSystem;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class AdminCommunicator : MonoBehaviour {

	private Vector2Int previousPosition = new Vector2Int(999999999, 999999999);

	private void Update() {
		Vector2Int position = Player.instance.transform.position.xz().ToVector2Int();

		if (position == previousPosition) {
			return;
		}
		
		var payload = new Dictionary<string, object>() {
			{"x", position.x},
			{"y", position.y}
		};

		MessageManager.instance.SendMessage(new Message("player/position", payload));
		
		previousPosition = position;
	}
}
