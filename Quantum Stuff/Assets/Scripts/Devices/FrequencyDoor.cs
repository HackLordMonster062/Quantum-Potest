using UnityEngine;

public class FrequencyDoor : MonoBehaviour {
	[SerializeField] int frequency;
	public int Frequency => frequency;

	MeshRenderer _renderer;

	private void Start() {
		_renderer = GetComponent<MeshRenderer>();

		_renderer.material.color = VisualManager.instance.FrequencyToColor(frequency);
	}

	public void Annihilate() {
		Destroy(gameObject);
	}
}
