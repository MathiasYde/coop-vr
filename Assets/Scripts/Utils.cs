using UnityEngine;

public static class Utils {
	public static bool TryRaycast(Vector3 origin, Vector3 direction, out RaycastHit hit, float distance,
		LayerMask layerMask) {
		Debug.DrawRay(origin, direction * distance);
		return Physics.Raycast(origin, direction, out hit, distance, layerMask);
	}
}
