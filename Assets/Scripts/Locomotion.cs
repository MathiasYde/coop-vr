using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Locomotion : MonoBehaviour {
	[SerializeField] private SteamVR_Action_Vector2 _movementAction;
	[SerializeField] private Rigidbody _head;

	[SerializeField] private float _sweepDistance = 0.5f;
	[SerializeField] private float _speed = 10.0f;

	[SerializeField] private LayerMask _layerMask;

	private void Start() {
		Debug.Assert(_movementAction != null, "Locomotion._movementAction is null", this);
		Debug.Assert(_head != null, "Locomotion._head is null", this);
	}

	private void Update() {
		Vector3 movement = new Vector3(_movementAction.axis.x, 0f, _movementAction.axis.y);
		Vector3 forward = Player.instance.hmdTransform.TransformDirection(movement);

		Vector3 direction = _speed * Vector3.ProjectOnPlane(forward, Vector3.up) * Time.deltaTime;

		RaycastHit hit;
		if (!Utils.TryRaycast(Player.instance.hmdTransform.position, forward, out hit, _sweepDistance, _layerMask)) {
			transform.position += direction;
		}        
	}

	private void OnDrawGizmos() {
		Gizmos.DrawRay(_head.position, Player.instance.bodyDirectionGuess * _sweepDistance);
	}
}