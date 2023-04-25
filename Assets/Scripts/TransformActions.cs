using UnityEngine;

public class TransformActions : MonoBehaviour {
	public void MoveTo(Transform target) {
		transform.position = target.position;
	}

	public void LocalMoveX(float x) {
		transform.position = transform.position + new Vector3(x, 0, 0);
	}
}