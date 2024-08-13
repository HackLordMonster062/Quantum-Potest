using UnityEngine;

public class PhotonShooter : MonoBehaviour {
    [SerializeField] Rigidbody photonPrefab;
    [SerializeField] Transform shootingPoint;
    [SerializeField] float shootingInterval;

    float _timer;

    void Start() {
        
    }

    void Update() {
        _timer -= Time.deltaTime;

        if (_timer <= 0) {
            _timer = shootingInterval;

            Instantiate(photonPrefab, shootingPoint.position, shootingPoint.rotation);
        }
    }
}
