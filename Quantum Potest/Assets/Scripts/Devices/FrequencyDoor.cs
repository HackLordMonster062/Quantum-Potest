using UnityEngine;

public class FrequencyDoor : MonoBehaviour, ISerializableElement {
	[SerializeField] int frequency;
	public int Frequency => frequency;

	MeshRenderer _renderer;

	private void Start() {
		_renderer = GetComponentInChildren<MeshRenderer>();

		MaterialPropertyBlock mpb = new();
		mpb.SetColor("_BaseColor", VisualManager.instance.FrequencyToColor(frequency));

		_renderer.SetPropertyBlock(mpb);
	}

	public void Annihilate() {
		Destroy(gameObject);
	}

	public ElementData Serialize() {
		return new FrequencyDoorData("ColoredDoor", transform.localPosition, transform.eulerAngles, gameObject.GetEntityId().ToString(), frequency);
	}

	public void Deserialize(ElementData data) {
		transform.localPosition = data.Position;
		transform.eulerAngles = data.Rotation;
		SetFrequency(((FrequencyDoorData)data).Frequency);
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
