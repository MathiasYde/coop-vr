using UnityEngine;

public static class Extensions {
	private static float LIGHT_COLOR_LERP_AMOUNT = 0.2f;
	private static Color LIGHT_WHITE_COLOR = Color.Lerp(Color.white, Color.black, LIGHT_COLOR_LERP_AMOUNT);
	private static Color LIGHT_BLUE_COLOR = Color.Lerp(Color.blue, Color.black, LIGHT_COLOR_LERP_AMOUNT);
	private static Color LIGHT_GREEN_COLOR = Color.Lerp(Color.green, Color.black, LIGHT_COLOR_LERP_AMOUNT);
	private static Color LIGHT_YELLOW_COLOR = Color.Lerp(Color.yellow, Color.black, LIGHT_COLOR_LERP_AMOUNT);
	private static Color LIGHT_RED_COLOR = Color.Lerp(Color.red, Color.black, LIGHT_COLOR_LERP_AMOUNT);
	
	public static Color? ToColor(this LightColor color) {
		switch (color) {
			case LightColor.White: return LIGHT_WHITE_COLOR;
			case LightColor.Blue: return LIGHT_BLUE_COLOR;
			case LightColor.Green: return LIGHT_GREEN_COLOR;
			case LightColor.Yellow: return LIGHT_YELLOW_COLOR;
			case LightColor.Red: return LIGHT_RED_COLOR;
			default: return null;
		}
	}
	
	private static Color FLASK_RED_COLOR = new Color(0.8f, 0.2f, 0.2f);
	private static Color FLASK_ORANGE_COLOR = new Color(0.8f, 0.5f, 0.2f);
	private static Color FLASK_YELLOW_COLOR = new Color(0.8f, 0.8f, 0.2f);
	private static Color FLASK_GREEN_COLOR = new Color(0.2f, 0.8f, 0.2f);
	private static Color FLASK_BLUE_COLOR = new Color(0.2f, 0.2f, 0.8f);
	private static Color FLASK_PURPLE_COLOR = new Color(0.52f, 0.0f, 1.0f);
	
	public static Color? ToColor(this FlaskColor color) {
		switch (color) {
			case FlaskColor.Red: return FLASK_RED_COLOR;
			case FlaskColor.Orange: return FLASK_ORANGE_COLOR;
			case FlaskColor.Yellow: return FLASK_YELLOW_COLOR;
			case FlaskColor.Green: return FLASK_GREEN_COLOR;
			case FlaskColor.Blue: return FLASK_BLUE_COLOR;
			case FlaskColor.Purple: return FLASK_PURPLE_COLOR;
			default: return null;
		}
	}
	
	public static Vector2Int ToVector2Int(this Vector2 vector) => new Vector2Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
	
	public static Vector2 xy(this Vector3 vector) => new Vector2(vector.x, vector.y);
	public static Vector2 xz(this Vector3 vector) => new Vector2(vector.x, vector.z);
	public static Vector2 yx(this Vector3 vector) => new Vector2(vector.y, vector.x);
	public static Vector2 yz(this Vector3 vector) => new Vector2(vector.y, vector.z);
	public static Vector2 zx(this Vector3 vector) => new Vector2(vector.z, vector.x);
	public static Vector2 zy(this Vector3 vector) => new Vector2(vector.z, vector.y);
}
