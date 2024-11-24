using System.Collections;
using UnityEngine;

public class PhotonShooter : Activatable {
    [SerializeField] float shootingPointDistance;
	[SerializeField] bool isStream;
	[SerializeField] float streamRate;

	private void Start() {
		if (isStream) StartCoroutine(PhotonStream());
	}

	public override void Activate() {
		Vector3 shootingPoint = transform.position + transform.forward * shootingPointDistance;

		Instantiate(PrefabManager.instance.Particles.Photon, shootingPoint, transform.rotation);
	}

	IEnumerator PhotonStream() {
		while (true) {
			yield return new WaitForSeconds(streamRate);

			Activate();
		}
	}
}
