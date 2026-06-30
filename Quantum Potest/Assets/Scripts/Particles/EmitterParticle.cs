using UnityEngine;

public class EmitterParticle : Particle, ISerializableElement<EmitterData> {
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

	public EmitterData Serialize() {
		Rotateable rotateable = GetComponent<Rotateable>();

		return new EmitterData(transform.position, transform.eulerAngles, Energy, rotateable == null ? Spin.Horizontal : rotateable.Spin);
	}

	public void Deserialize(EmitterData data) {
		Rotateable rotateable = GetComponent<Rotateable>();

		transform.position = data.Position;
		transform.eulerAngles = data.Rotation;
		Energy = data.Energy;

		if (rotateable != null) rotateable.SetSpin(data.Spin);
	}
}