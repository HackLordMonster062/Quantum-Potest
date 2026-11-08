using System.Collections;
using UnityEngine;

public class PhotonShooter : Activatable, ISerializableElement {
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

	public ElementData Serialize() {
		return new ActivatableData("PhotonShooter", transform.localPosition, transform.eulerAngles, transform.localScale, gameObject.GetEntityId().ToString(), _trigger == null ? "" : _trigger.gameObject.GetEntityId().ToString());
	}

	public void Deserialize(ElementData data) {
		transform.localPosition = data.Position;
		transform.eulerAngles = data.Rotation;
		transform.localScale = data.Scale;
	}
}
