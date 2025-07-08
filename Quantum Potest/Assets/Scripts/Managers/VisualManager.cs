using UnityEngine;

public class VisualManager : Singleton<VisualManager> {
	[SerializeField] int maxFrequency;
	public int MaxFrequency { get { return maxFrequency; } }

	[SerializeField] float colorSwitchTime;
	public float ColorSwitchTime { get { return colorSwitchTime; } }

	public Color FrequencyToColor(int frequency) {
		return Color.HSVToRGB(frequency / (float)maxFrequency, 1f, 1f);
	}
}
