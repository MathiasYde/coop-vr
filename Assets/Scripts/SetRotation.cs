using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRotation : MonoBehaviour {
	public void SetRotationFromMessage(MessageSystem.Message message) {
		Debug.Log("HELLO");
		
		Vector3 rotation = new Vector3(
			(float)message.payload["gx"],
			(float)message.payload["gy"],
			(float)message.payload["gz"]);

		transform.rotation = Quaternion.Euler(rotation);
	}
}
