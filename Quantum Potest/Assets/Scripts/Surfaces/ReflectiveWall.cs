using UnityEngine;

public class ReflectiveWall : MonoBehaviour, ISerializableElement {
	public ElementData Serialize() {
		return new SurfaceData("ReflectiveWalls", transform.position, transform.eulerAngles, transform.localScale);
	}

	public void Deserialize(ElementData data) {
		transform.position = data.Position;
		transform.eulerAngles = data.Rotation;
		transform.localScale = ((SurfaceData)data).Scale;
	}
}
