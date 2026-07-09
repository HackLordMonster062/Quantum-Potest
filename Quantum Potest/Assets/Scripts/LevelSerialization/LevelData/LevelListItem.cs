using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelListItem {
	public string Name { get; private set; }

	public List<LevelData> Versions { get; private set; }
	public int CurrVersionIndex { get; private set; }
	[JsonIgnore] public LevelData CurrVersion => Versions[CurrVersionIndex];

	public LevelListItem(string name, List<LevelData> versions, int currVersionIndex = 0) {
		Name = name;
		Versions = versions;
		CurrVersionIndex = currVersionIndex;
	}

	public void SetVersion(int newVersion) {
		CurrVersionIndex = newVersion;
	}

	public void AddVersion(LevelData data) {
		Versions.Add(data);
	}

	public void Rename(string newName) {
		Name = newName;
	}

	public void UpdateCurrentVersion(LevelData data) {
		Versions ??= new List<LevelData>();

		if (CurrVersionIndex < 0 || CurrVersionIndex >= Versions.Count) {
			Versions.Add(data);
			CurrVersionIndex = Versions.Count - 1;
		} else {
			Versions[CurrVersionIndex] = data;
		}
	}
}
