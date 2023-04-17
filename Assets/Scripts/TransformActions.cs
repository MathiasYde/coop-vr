using UnityEngine;

public class TransformActions : MonoBehaviour {
	public void MoveTo(Transform target) {
		transform.position = target.position;
	}
}