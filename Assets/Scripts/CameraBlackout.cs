using System;
using UnityEngine;

public class CameraBlackout : MonoBehaviour {
	[SerializeField] private bool blackout;

	[SerializeField] private float maxWallDistance = 0.25f;
	[SerializeField] private Transform head;
	[SerializeField] private LayerMask layerMask;
	
	private Material fadeMaterial;
	private int fadeMaterialColorID = -1;
	private Vector3 returnPoint;
	private Vector3 returnNormal;

	private void Awake() {
		if (fadeMaterial == null) {
			fadeMaterial = new Material(Shader.Find("Custom/SteamVR_Fade"));
			fadeMaterialColorID = Shader.PropertyToID("fadeColor");
		}
	}

	private void Update() {
		if (blackout) {
			if (Vector3.Dot(returnNormal, head.position - returnPoint) > 0.0f) {
				blackout = false;
			}
		}
		
		if (!blackout) { // player has sight
			// check for wall intersection
			RaycastHit hit;
			if (Utils.TryRaycast(head.position, head.forward, out hit, maxWallDistance, layerMask)) {
				returnPoint = hit.point;
				returnNormal = hit.normal;
				blackout = true;
			}
		}
		}

	private void OnDrawGizmos() {
		if (blackout) {
			Gizmos.color = Color.red;
			
			Gizmos.DrawSphere(returnPoint, 0.1f);
			Gizmos.DrawLine(returnPoint, head.position);
			Gizmos.DrawRay(returnPoint, returnNormal);
		}
	}

	private void OnPostRender() {
		if (!blackout) { return; }
			
		fadeMaterial.SetColor(fadeMaterialColorID, Color.black);
		fadeMaterial.SetPass(0);
		
		GL.Begin(GL.QUADS);
		GL.Vertex3(-1, -1, 0);
		GL.Vertex3(1, -1, 0);
		GL.Vertex3(1, 1, 0);
		GL.Vertex3(-1, 1, 0);
		GL.End();
	}
}
