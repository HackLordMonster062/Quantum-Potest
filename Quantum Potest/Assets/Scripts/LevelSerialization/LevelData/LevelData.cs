using System.Collections.Generic;
using UnityEngine;

public class LevelData {
	public Vector3 Size { get; private set; }
	public Vector3 Entrance { get; private set; }
	public Vector3 Exit { get; private set; }
	public List<ElementData> Elements { get; private set; }

	public LevelData(Vector3 size, Vector3 entrance, Vector3 exit, List<ElementData> elements) {
		Size = size;
		Entrance = entrance;
		Exit = exit;
		Elements = elements;
	}
}
