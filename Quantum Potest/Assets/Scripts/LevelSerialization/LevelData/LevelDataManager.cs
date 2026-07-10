using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class LevelDataManager : Singleton<LevelDataManager> {
    [field: SerializeField] public Material roomMaterial { get; private set; }
    [field: SerializeField] public Vector2 doorSize { get; private set; }
    [SerializeField] Vector3 defaultRoomSize;
    [SerializeField] Vector3 defaultEntrancePoint;
    [SerializeField] Vector3 defaultExitPoint;

    List<LevelListItem> _levels;
    List<Level> _physicalLevels;

    int _currentLevel = 0;

    public Level CurrentLevel => _physicalLevels[_currentLevel];
    public int CurrentLevelIndex => _currentLevel;

    public event Action<int, string, int, int> OnLevelInfoChanged;
	
	void Start() {
        _levels = new();
        _physicalLevels = new();

        LoadAllLevels();
    }

    public void TeleportPlayer(int index) {
        GameManager.instance.Player.position = _physicalLevels[index].transform.position + _levels[index].CurrVersion.Entrance + Vector3.forward;

		UpdateInfo();
	}

    public void AddLevel(int index, LevelListItem data) {
        index = Mathf.Clamp(index, 0, _levels.Count);

        _levels.Insert(index, data);

        ReconstructLevels();
    }

	public void AddLevel(int index, string name, LevelData data) {
        AddLevel(index, new LevelListItem(name, new() { data }));

		UpdateInfo();
	}

	public void NewLevelAtEnd() {
        AddLevel(_levels.Count, "New Level", new LevelData(defaultRoomSize, defaultEntrancePoint, defaultExitPoint, new()));

		UpdateInfo();
	}

    public void NewLevelAfterCurrent() {
        AddLevel(_currentLevel + 1, "New Level", new LevelData(defaultRoomSize, defaultEntrancePoint, defaultExitPoint, new()));

		UpdateInfo();
	}

    public void RenameCurrentLevel(string newName) {
        CurrentLevel.gameObject.name = newName;
        ScrapeLevels();

		UpdateInfo();
	}

    public void RemoveCurrentLevel() {
        _levels.RemoveAt(_currentLevel);

        SetCurrentLevel(Mathf.Min(_currentLevel, _levels.Count - 1));

        ReconstructLevels();
        TeleportPlayer(_currentLevel);

		UpdateInfo();
	}

    public void DuplicateCurrentLevel() {
        AddLevel(_currentLevel + 1, _levels[_currentLevel].Name, _levels[_currentLevel].CurrVersion.Copy());

		UpdateInfo();
	}

    public void AddLevelVersion() {
        _levels[_currentLevel].AddVersion(_levels[_currentLevel].CurrVersion.Copy());
        SetLevelVersion(_levels[_currentLevel].Versions.Count - 1);
    }

    public void RemoveCurrLevelVersion() {
        if (_levels[_currentLevel].Versions.Count <= 1) {
            RemoveCurrentLevel();
            return;
        }

        _levels[_currentLevel].RemoveCurrVersion();
		ReconstructLevels();
		UpdateInfo();
	}

    public void SetLevelVersion(int newVersion) {
        _levels[_currentLevel].SetVersion(newVersion);
        ReconstructLevels();
        UpdateInfo();
    }

    public void CycleLevelVersions() {
        SetLevelVersion((_levels[_currentLevel].CurrVersionIndex + 1) % _levels[_currentLevel].Versions.Count);
	}

    public void ReloadLevelPosition(Level level, Vector3 delta, bool moveSelf) {
        ScrapeLevels();
        ReconstructLevels();

  //      int index = _physicalLevels.IndexOf(level);

  //      if (index == -1) return;

  //      if (moveSelf) {
  //          for (int i = index; i < _physicalLevels.Count; i++) {
  //              _physicalLevels[i].transform.position -= delta;
  //          }
  //      } else {
		//	for (int i = index + 1; i < _physicalLevels.Count; i++) {
		//		_physicalLevels[i].transform.position += delta;
		//	}
		//}
	}

    public void MoveCurrentLevel(int newIndex) {
        newIndex = Mathf.Clamp(newIndex, 0, _levels.Count - 1);

        LevelListItem current = _levels[_currentLevel];
        _levels.RemoveAt(_currentLevel);
        _levels.Insert(newIndex, current);

        SetCurrentLevel(newIndex);

        ReconstructLevels();
        TeleportPlayer(_currentLevel);
		UpdateInfo();
	}

    public void ReconstructLevels() {
        foreach (Level level in _physicalLevels) {
            Destroy(level.gameObject);
        }

        ConstructLevelList(_levels);

        SetCurrentLevel(_currentLevel);
		UpdateInfo();
	}

    public void ReloadLevels() {
        LoadAllLevels();
        ReconstructLevels();
    }

    public void ScrapeLevels() {
        for (int i = 0; i < _levels.Count; i++) {
            LevelData data = _physicalLevels[i].GetLevelData();

			_levels[i].UpdateCurrentVersion(data);
            _levels[i].Rename(_physicalLevels[i].name);
        }
    }

    public void ConstructLevelList(List<LevelListItem> levelList) {
        (Level lastRoom, LevelData lastLevel) = (null, null);

        _physicalLevels = new();

        foreach (LevelListItem data in levelList) {
            Level room = ConstructLevel(data.CurrVersion, data.Name);
            _physicalLevels.Add(room.GetComponent<Level>());

            if (lastRoom != null) {
                room.transform.position = lastRoom.transform.position + lastLevel.Exit - data.CurrVersion.Entrance;
            }

            (lastRoom, lastLevel) = (room, data.CurrVersion);
        }
    }

    public Level ConstructLevel(LevelData data, string name) {
        Level level = Instantiate(PrefabManager.instance.Level).GetComponent<Level>();
        level.DataUpdate(data.Entrance, data.Exit, data.Size);
        level.OnHandleMoved += ReloadLevelPosition;
        level.OnPlayerEnter += SetCurrentLevel;
        level.gameObject.name = name;

        Dictionary<string, (DeviceData, GameObject)> deviceLookup = new();

        foreach (ElementData element in data.Elements) {
			switch (element) {
                case DeviceData device:
					GameObject physical = Instantiate(PrefabManager.instance.GetDevice(element.PrefabID), element.Position, Quaternion.Euler(element.Rotation), level.transform);

					deviceLookup[device.ID] = (device, physical);
                    break;
                case ParticleData particle:
					physical = Instantiate(PrefabManager.instance.GetParticle(element.PrefabID), element.Position, Quaternion.Euler(element.Rotation), level.transform);

                    if (particle.Energy > 0)
					    physical.GetComponent<Excitable>().Excite(particle.Energy, false);

                    switch (particle) {
                        case EmitterData emitter:
                            physical.GetComponent<Rotateable>().SetSpin(emitter.Spin);
                            break;
                        case SpectronData spectron:
                            physical.GetComponent<Spectron>().SetFrequencies(spectron.Frequencies);
                            break;
                        default:
                            break;
                    }

                    break;
                case SurfaceData surface:
					physical = Instantiate(PrefabManager.instance.GetSurface(element.PrefabID), element.Position, Quaternion.Euler(element.Rotation), level.transform);

					physical.transform.localScale = surface.Scale;

                    if (surface is SlidingWallData slidingWall) {
                        Transform point1 = Instantiate(PrefabManager.instance.Devices.RailPoint, slidingWall.Point1, Quaternion.identity).transform;
                        Transform point2 = Instantiate(PrefabManager.instance.Devices.RailPoint, slidingWall.Point2, Quaternion.identity).transform;

                        physical.GetComponent<CapturableBlock>().Initialize(point1, point2, slidingWall.Orient);
                    }

                    break;
                default:
                    break;
            }
        }

        foreach (var (id, (eData, obj)) in deviceLookup) {
			switch (eData) {
				case ActivatableData device:
                    if (device.TriggerID != "") {
                        obj.GetComponent<Activatable>().SetTrigger(deviceLookup[device.TriggerID].Item2.GetComponent<Trigger>());
                    }

                    if (device is RailData rail) {
                        Transform[] path = rail.Path.Select(point => Instantiate(PrefabManager.instance.Devices.RailPoint, point, Quaternion.identity).transform).ToArray();

                        obj.GetComponent<Rail>().Initialize(path, deviceLookup[rail.DeviceID].Item2.transform);
                    }

					break;
                case FrequencyDoorData door:
                    obj.GetComponent<FrequencyDoor>().SetFrequency(door.Frequency);
                    break;
				default:
					break;
			}
		}

        return level;
    }

    public void SetCurrentLevel(Level level) {
		int index = _physicalLevels.IndexOf(level);

        SetCurrentLevel(index);
	}

	public void SetCurrentLevel(int newIndex) {
        CurrentLevel.SetActive(false);

		_currentLevel = newIndex;

        CurrentLevel.SetActive(true);

		UpdateInfo();
	}

	public void UpdateInfo() {
        LevelListItem data = _levels[_currentLevel];

        OnLevelInfoChanged?.Invoke(_currentLevel, data.Name, data.CurrVersionIndex, data.Versions.Count);
    }

	JsonSerializerSettings settings = new JsonSerializerSettings {
		TypeNameHandling = TypeNameHandling.Auto,
		Formatting = Formatting.Indented,
		ContractResolver = new UnityFieldsOnlyContractResolver()
	};

	string GetSavePath() {
		string folderPath = Path.Combine(Application.persistentDataPath, "Levels");

		if (!Directory.Exists(folderPath)) {
			Directory.CreateDirectory(folderPath);
		}

        return folderPath;
	}
  
    void SaveLevel(LevelListItem level, string name) {
        string filePath = Path.Combine(GetSavePath(), $"{name}.json");

		string json = JsonConvert.SerializeObject(level, settings);

        File.WriteAllText(filePath, json);
    }

    public void SaveLevels() {
        Directory.Delete(GetSavePath(), true);

        for (int i = 0; i < _levels.Count; i++) {
            SaveLevel(_levels[i], $"{i}``{_levels[i].Name}");
        }
    }

    void LoadAllLevels() {
        string folderPath = GetSavePath();

        _levels = new();

		foreach (string filePath in Directory.EnumerateFiles(folderPath)) {
            string json = File.ReadAllText(filePath);

            LevelListItem level = JsonConvert.DeserializeObject<LevelListItem>(json, settings);

            string[] parts = filePath.Split("\\").Last().Split("``");
            int index = int.Parse(parts[0]);

            _levels.Insert(index, level);
        }

        ReconstructLevels();
	}
}
