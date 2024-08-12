using UnityEngine;

public static class Vector3Extensions {
	public static Vector3 Modify(this Vector3 v, float? x = null, float? y = null, float? z = null) {
		return new Vector3(x ?? v.x, y ?? v.y, z ?? v.z);
	}
}
