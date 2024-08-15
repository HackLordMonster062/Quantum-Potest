using UnityEngine;

public class FrequencyDoor : MonoBehaviour, IInteractable {
	[SerializeField] int frequency;

	MeshRenderer _renderer;

	private void Start() {
		_renderer = GetComponent<MeshRenderer>();

		_renderer.material.color = VisualManager.instance.FrequencyToColor(frequency);
	}

	public bool Interact(ILiftable item) {
		ColorParticle key = item as ColorParticle;

		if (key != null && key.HasFrequency(frequency)) {
			gameObject.SetActive(false);
			return true;
		}

		return false;
    }
}
