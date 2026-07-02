using System;
using UnityEngine;

[Serializable]
public abstract class ElementData {
	public string PrefabID { get; protected set; }
	public Vector3 Position { get; protected set; }
	public Vector3 Rotation { get; protected set; }
}
