using UnityEngine;

public class FrequencyDoor : MonoBehaviour, ISerializableElement<FrequencyDoorData> {
	[SerializeField] int frequency;
	public int Frequency => frequency;

	MeshRenderer _renderer;

	private void Start() {
		_renderer = GetComponent<MeshRenderer>();

		MaterialPropertyBlock mpb = new();
		mpb.SetColor("_BaseColor", VisualManager.instance.FrequencyToColor(frequency));

		_renderer.SetPropertyBlock(mpb);
	}

	public void Annihilate() {
		Destroy(gameObject);
	}

	public FrequencyDoorData Serialize() {
		return new FrequencyDoorData("ColoredDoor", transform.position, transform.eulerAngles, gameObject.GetEntityId(), frequency);
	}

	public void Deserialize(FrequencyDoorData data) {
		transform.position = data.Position;
		transform.eulerAngles = data.Rotation;
		SetFrequency(data.Frequency);
	}

#if UNITY_EDITOR
	public void SetFrequency(int frequency) {
		this.frequency = frequency;
		if (_renderer != null) {
			MaterialPropertyBlock mpb = new();
			mpb.SetColor("_BaseColor", VisualManager.instance.FrequencyToColor(frequency));

			_renderer.SetPropertyBlock(mpb);
		}
	}
#endif
}
