using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRotation : MonoBehaviour {
	public void SetRotationFromMessage(MessageSystem.Message message) {
		Vector3 rotation = message.GetVector3("gx", "gy", "gz");
		transform.rotation = Quaternion.Euler(rotation);
	}
}
