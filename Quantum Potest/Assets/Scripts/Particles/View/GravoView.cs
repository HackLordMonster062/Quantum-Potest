using System;
using UnityEngine;
using UnityEngine.VFX;

public class GravoView : MonoBehaviour {
    [SerializeField] VisualEffect vfx;
    
	public SlotData[] slots;

	GraphicsBuffer buffer;

	void OnEnable() {
		buffer = new GraphicsBuffer(
			GraphicsBuffer.Target.Structured,
			26,
			sizeof(float) * 3 + sizeof(int)
		);
	}

	void OnDisable() {
		buffer?.Release();
		buffer = null;
	}

	void Update() {
        buffer.SetData(slots);
		vfx.SetGraphicsBuffer("SlotPositions", buffer);
	}

	[VFXType(VFXTypeAttribute.Usage.GraphicsBuffer)]
    [Serializable]
	public struct SlotData {
        public Vector3 position;
        public int isTaken;

		public override string ToString() {
			return $"Position: {position}, IsTaken: {isTaken}";
		}
    }
}
