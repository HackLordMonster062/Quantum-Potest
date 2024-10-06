using UnityEngine;

public class EmitterParticle : Excitable {
	[SerializeField] float shootingPointDistance;

	protected override void Update() {
		base.Update();
	}

	void ShootPhoton() {
        Vector3 shootingPoint = transform.position + transform.forward * shootingPointDistance;

		Instantiate(PrefabManager.instance.Particles.Photon, shootingPoint, transform.rotation);
	}

	protected override void Decay() {
		base.Decay();

        ShootPhoton();
	}
}