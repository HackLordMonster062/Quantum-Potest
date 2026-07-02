using UnityEngine;

public class Wall : MonoBehaviour, ISerializableElement<SurfaceData> {
	public SurfaceData Serialize() {
		return new SurfaceData("Wall", transform.position, transform.eulerAngles, transform.localScale);
	}

	public void Deserialize(SurfaceData data) {
		transform.position = data.Position;
		transform.eulerAngles = data.Rotation;
		transform.localScale = data.Scale;
	}
}
