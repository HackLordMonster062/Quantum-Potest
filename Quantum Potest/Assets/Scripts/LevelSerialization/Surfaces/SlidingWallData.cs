using System;
using UnityEngine;

public class SlidingWallData : SurfaceData {
	public Vector3 Point1 { get; private set; }
	public Vector3 Point2 { get; private set; }
	public bool Orient { get; private set; }

	public SlidingWallData(string prefabId, Vector3 position, Vector3 rotation, Vector3 scale, Vector3 point1, Vector3 point2, bool orient) : base(prefabId, position, rotation, scale) {
		Point1 = point1;
		Point2 = point2;
		Orient = orient;
	}
}
