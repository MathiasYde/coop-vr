using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public static class Utils {
	
	private static Random rng = new Random();  

	public static void Shuffle<T>(this IList<T> list)  
	{  
		int n = list.Count;  
		while (n > 1) {  
			n--;  
			int k = rng.Next(n + 1);  
			T value = list[k];  
			list[k] = list[n];  
			list[n] = value;  
		}  
	}
	public static bool TryRaycast(Vector3 origin, Vector3 direction, out RaycastHit hit, float distance,
		LayerMask layerMask) {
		Debug.DrawRay(origin, direction * distance);
		return Physics.Raycast(origin, direction, out hit, distance, layerMask);
	}
}
