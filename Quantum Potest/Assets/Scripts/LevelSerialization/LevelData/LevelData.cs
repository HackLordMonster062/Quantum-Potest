using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelData {
	public string Name { get; private set; }
	public Vector3 Size { get; private set; }
	public Vector3 Entrance { get; private set; }
	public Vector3 Exit { get; private set; }
	public List<ElementData> Elements { get; private set; }

	public LevelData(string name, Vector3 size, Vector3 entrance, Vector3 exit, List<ElementData> elements) {
		Name = name;
		Size = size;
		Entrance = entrance;
		Exit = exit;
		Elements = elements;
	}

	public LevelData Copy() {
		return new(
			Name,
			Size,
			Entrance,
			Exit,
			Elements.ToList()
		);
	}
}
