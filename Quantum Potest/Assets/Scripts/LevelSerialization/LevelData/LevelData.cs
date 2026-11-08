using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelData {
	public Vector3 Size { get; protected set; }
	public Vector3 Entrance { get; protected set; }
	public Vector3 Exit { get; protected set; }
	public List<ElementData> Elements { get; protected set; }

	public LevelData(Vector3 size, Vector3 entrance, Vector3 exit, List<ElementData> elements) {
		Size = size;
		Entrance = entrance;
		Exit = exit;
		Elements = elements;
	}

	public LevelData Copy() {
		return new LevelData(
			Size,
			Entrance,
			Exit,
			Elements.ToList()
		);
	}
}
