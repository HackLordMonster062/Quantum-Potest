using UnityEngine;

public class LayeredRoom : MonoBehaviour {
    [SerializeField] Transform playerClonePrefab;

    [SerializeField] Transform[] rooms;

    Transform[] _playerLayers;
    bool[] _activeLayers;

    Transform _player;

    int _currRoomIndex;
    int _activeLayerCount;

    bool _isActive;

    void Start() {
        _activeLayerCount = rooms.Length;

        _playerLayers = new Transform[rooms.Length];
        _activeLayers = new bool[rooms.Length];

        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerLayers[0] = _player;

        _currRoomIndex = 0;

        for (int i = 0; i < rooms.Length; i++) {
            _activeLayers[i] = true;

            if (i == _currRoomIndex) continue;

            _playerLayers[i] = Instantiate(playerClonePrefab, _player);
            _playerLayers[i].gameObject.SetActive(false);
        }

		Toggle(true);
    }

    void Update() {
        if (!_isActive) return;

        if (Input.GetKeyDown(KeyCode.Q))
			NextRoom();

        if (Input.GetKeyDown(KeyCode.R))
            Prune(_currRoomIndex);
	}

    void UpdatePositions() {
		Vector3 relativePosition = _player.position - rooms[_currRoomIndex].position;

		for (int i = 0; i < rooms.Length; i++) {
			if (i == _currRoomIndex) continue;

			_playerLayers[i].position = rooms[i].position + relativePosition;
		}
	}

    void SwitchPosition(int index) {
        if (!_activeLayers[index]) return;

        Vector3 distance = _playerLayers[index].position - _player.position;

		_player.position += distance;

		for (int i = 0; i < rooms.Length; i++) {
			if (i == _currRoomIndex) continue;

            if (i == index) {
                _playerLayers[i].position -= distance;
            }

			_playerLayers[i].position -= distance;
		}

		_playerLayers[_currRoomIndex] = _playerLayers[index];
        _playerLayers[index] = _player;

        _currRoomIndex = index;
    }

    void NextRoom() {
        int index = _currRoomIndex;

        do
            index = (index + 1) % rooms.Length;
        while (!_activeLayers[index]);

		SwitchPosition(index);
	}

    void Prune(int index) {
        if (index == _currRoomIndex) {
            NextRoom();
        }

        _activeLayers[index] = false;
        _playerLayers[index].gameObject.SetActive(false);

        _activeLayerCount--;

        if (_activeLayerCount <= 1)
            Toggle(false);
    }

    void Toggle(bool active) {
        _isActive = active;

        for (int i = 0; i < rooms.Length; i++) {
            if (i == _currRoomIndex) continue;

            _playerLayers[i].gameObject.SetActive(active);
        }

        UpdatePositions();
	}
}
