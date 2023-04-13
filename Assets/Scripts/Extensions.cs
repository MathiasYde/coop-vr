using UnityEngine;

public static class Extensions {
	private static float COLOR_LERP_AMOUNT = 0.2f;
	private static Color WHITE_COLOR = Color.Lerp(Color.white, Color.black, COLOR_LERP_AMOUNT);
	private static Color BLUE_COLOR = Color.Lerp(Color.blue, Color.black, COLOR_LERP_AMOUNT);
	private static Color GREEN_COLOR = Color.Lerp(Color.green, Color.black, COLOR_LERP_AMOUNT);
	private static Color YELLOW_COLOR = Color.Lerp(Color.yellow, Color.black, COLOR_LERP_AMOUNT);
	private static Color RED_COLOR = Color.Lerp(Color.red, Color.black, COLOR_LERP_AMOUNT);
	
	public static Color? ToColor(this LightColor color) {
		switch (color) {
			case LightColor.White: return WHITE_COLOR;
			case LightColor.Blue: return BLUE_COLOR;
			case LightColor.Green: return GREEN_COLOR;
			case LightColor.Yellow: return YELLOW_COLOR;
			case LightColor.Red: return RED_COLOR;
			default: return null;
		}
	}		
}
