using UnityEngine;

public class FrequencyDoor : MonoBehaviour, IInteractable {
	[SerializeField] int frequency;

	MeshRenderer _renderer;

	private void Awake() {
		_renderer = GetComponent<MeshRenderer>();

		_renderer.material.color = Color.HSVToRGB(1f / frequency, 1, 1);
	}

	public bool Interact(ILiftable item) {
		ColorParticle key = item as ColorParticle;

		if (key != null && frequency == key.Frequency) {
			gameObject.SetActive(false);
			return true;
		}

		return false;
    }
}
