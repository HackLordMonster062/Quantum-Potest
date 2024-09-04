using UnityEngine;

public class EmitterParticle : Excitable {

	protected override void Update() {
		base.Update();

		transform.eulerAngles = transform.eulerAngles.Modify(x: 0, z: 0);
	}

	void ShootPhoton() {
        Vector3 shootingPoint = transform.position + transform.forward;

		Instantiate(PrefabManager.instance.Particles.Photon, shootingPoint, transform.rotation);
	}

	protected override void Deplete() {
		base.Deplete();

        ShootPhoton();
	}
}
