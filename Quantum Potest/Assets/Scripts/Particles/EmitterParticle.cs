using UnityEngine;

[RequireComponent(typeof(Rotateable))]
public class EmitterParticle : Particle, ISerializableElement {
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

	public ElementData Serialize() {
		Rotateable rotateable = GetComponent<Rotateable>();

		return new EmitterData(transform.localPosition, transform.eulerAngles, Energy, rotateable.Spin);
	}

	public void Deserialize(ElementData data) {
		EmitterData casted = (EmitterData)data;

		Rotateable rotateable = GetComponent<Rotateable>();

		transform.localPosition = casted.Position;
		transform.eulerAngles = casted.Rotation;
		Energy = casted.Energy;

		rotateable.SetSpin(casted.Spin);
	}
}