using System.Collections;
using UnityEngine;

public class PhotonShooter : Activatable, ISerializableElement<ActivatableData> {
    [SerializeField] float shootingPointDistance;
	[SerializeField] bool isStream;
	[SerializeField] float streamRate;

	private void Start() {
		if (isStream) StartCoroutine(PhotonStream());
	}

	public override void Activate(int _ = 0) {
		Vector3 shootingPoint = transform.position + transform.forward * shootingPointDistance;

		Instantiate(PrefabManager.instance.Particles.Photon, shootingPoint, transform.rotation);
	}

	IEnumerator PhotonStream() {
		while (true) {
			yield return new WaitForSeconds(streamRate);

			Activate();
		}
	}

	public ActivatableData Serialize() {
		return new ActivatableData("PhotonShooter", transform.position, transform.eulerAngles, gameObject.GetEntityId(), _trigger.gameObject.GetEntityId());
	}

	public void Deserialize(ActivatableData data) {
		transform.position = data.Position;
		transform.eulerAngles = data.Rotation;
	}
}
