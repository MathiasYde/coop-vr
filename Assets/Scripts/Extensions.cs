using UnityEngine;

public static class Extensions {
	public static Color? ToColor(this LightColor color) {
		switch (color) {
			case LightColor.White: return Color.white;
			case LightColor.Blue: return Color.blue;
			case LightColor.Green: return Color.green;
			case LightColor.Yellow: return Color.yellow;
			case LightColor.Red: return Color.red;
			default: return null;
		}
	}		
}
