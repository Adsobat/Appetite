using System.Collections;
using UnityEngine;

public static class ExtensionMethods {

	public static Vector2 TransPolarToKar (this Vector2 v) {
		return new Vector2 (v.x * Mathf.Cos (v.y), v.x * Mathf.Sin (v.y));
	}
	public static Vector2 TransKarToPolar (this Vector2 v) {
		// r = sqr (x^2+ y^2)
		float r = Mathf.Sqrt((v.x*v.x) + (v.y*v.y) );
		float alpha = Mathf.Acos( v.x / r);
		return new Vector2 (r , alpha);
	}
}