using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelDataManager : Singleton<LevelDataManager> {
    [field: SerializeField] public Material roomMaterial { get; private set; }
    [field: SerializeField] public Vector2 doorSize { get; private set; }
    [SerializeField] Vector3 defaultRoomSize;
    [SerializeField] Vector3 defaultEntrancePoint;
    [SerializeField] Vector3 defaultExitPoint;

    List<LevelData> _levels;
    List<Level> _physicalLevels;

    int _currentLevel = 0;
	
	void Start() {
        ConstructLevelList(_levels);
    }

    void Update() {
        
    }

    public void TeleportPlayer(int index) {
        GameManager.instance.Player.position = _physicalLevels[index].transform.position + _levels[index].Entrance + Vector3.forward;
    }

    public void AddLevel(int index, LevelData data) {
        index = Mathf.Clamp(index, 0, _levels.Count);

        _levels.Insert(index, data);

        ReconstructLevels();
    }

    public void NewLevelAtEnd() {
        AddLevel(_levels.Count, new LevelData(defaultRoomSize, defaultEntrancePoint, defaultExitPoint, new()));
    }

    public void NewLevelAfterCurrent() {
        AddLevel(_currentLevel + 1, new LevelData(defaultRoomSize, defaultEntrancePoint, defaultExitPoint, new()));
    }

    public void RemoveCurrentLevel() {
        _levels.RemoveAt(_currentLevel);

        _currentLevel = Mathf.Min(_currentLevel, _levels.Count);

        ReconstructLevels();
        TeleportPlayer(_currentLevel);
    }

    public void DuplicateCurrentLevel() {
        AddLevel(_currentLevel + 1, _levels[_currentLevel].Copy());
    }

    public void ReloadLevelPosition(Level level, Vector3 delta, bool moveSelf) {
        int index = _physicalLevels.IndexOf(level);

        if (index == -1) return;

        if (moveSelf) {
            for (int i = index; i < _physicalLevels.Count; i++) {
                _physicalLevels[i].transform.position -= delta;
            }
        } else {
			for (int i = index + 1; i < _physicalLevels.Count; i++) {
				_physicalLevels[i].transform.position += delta;
			}
		}
    }

    public void MoveCurrentLevel(int newIndex) {
        LevelData current = _levels[_currentLevel];
        _levels.RemoveAt(_currentLevel);
        _levels.Insert(newIndex, current);

        _currentLevel = newIndex;
        TeleportPlayer(_currentLevel);
    }

    public void ReconstructLevels() {
        foreach (Level level in _physicalLevels) {
            Destroy(level.gameObject);
        }

        ConstructLevelList(_levels);
    }

    public void ReloadLevels() {

    }

    public void ConstructLevelList(List<LevelData> levelList) {
        (Level lastRoom, LevelData lastLevel) = (null, null);

        _physicalLevels = new();

        foreach (LevelData data in levelList) {
            Level room = ConstructLevel(data);
            _physicalLevels.Add(room.GetComponent<Level>());

            if (lastRoom != null) {
                room.transform.position = lastRoom.transform.position + lastLevel.Exit - data.Entrance;
            }

            (lastRoom, lastLevel) = (room, data);
        }
    }

    public Level ConstructLevel(LevelData data) {
        Level level = Instantiate(PrefabManager.instance.Level).GetComponent<Level>();
        level.DataUpdate(data.Entrance, data.Exit, data.Size);
        level.OnHandleMoved += ReloadLevelPosition;
        level.OnPlayerEnter += SetCurrentLevel;

        Dictionary<string, (DeviceData, GameObject)> deviceLookup = new();

        foreach (ElementData element in data.Elements) {
			GameObject physical = Instantiate(PrefabManager.instance.GetDevice(element.PrefabID), element.Position, Quaternion.Euler(element.Rotation), level.transform);

			switch (element) {
                case DeviceData device:
                    deviceLookup[device.ID] = (device, physical);
                    break;
                case ParticleData particle:
                    physical.GetComponent<Excitable>().Excite(particle.Energy, false);
                    break;
                case SurfaceData surface:
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

        _currentLevel = index;
	}
}
