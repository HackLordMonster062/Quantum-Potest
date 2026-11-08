using UnityEngine;

public class ReflectiveWall : MonoBehaviour, ISerializableElement {
	public ElementData Serialize() {
		return new SurfaceData("ReflectiveWall", transform.localPosition, transform.eulerAngles, transform.localScale);
	}

	public void Deserialize(ElementData data) {
		transform.localPosition = data.Position;
		transform.eulerAngles = data.Rotation;
		transform.localScale = data.Scale;
	}
}
