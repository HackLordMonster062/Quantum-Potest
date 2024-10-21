using UnityEngine;

public class PhotonShooter : Activatable {
    [SerializeField] float shootingPointDistance;

	public override void Activate() {
		Vector3 shootingPoint = transform.position + transform.forward * shootingPointDistance;

		Instantiate(PrefabManager.instance.Particles.Photon, shootingPoint, transform.rotation);
	}
}
