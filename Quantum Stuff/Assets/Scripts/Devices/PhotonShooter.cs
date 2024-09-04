using UnityEngine;

public class PhotonShooter : MonoBehaviour {
    [SerializeField] Transform shootingPoint;
    [SerializeField] float shootingInterval;

    float _timer;
    GameObject _photonPrefab;

    void Start() {
        _photonPrefab = PrefabManager.instance.Particles.Photon;
	}

    void Update() {
        _timer -= Time.deltaTime;

        if (_timer <= 0) {
            _timer = shootingInterval;

            Instantiate(_photonPrefab, shootingPoint.position, shootingPoint.rotation);
        }
    }
}
