using System;
using UnityEngine;

[Serializable]
public class ParticleData : ElementData { 
	public int Energy { get; protected set; } // NOTE: If you intend to use this for mid-level saving, you also need to store the time since last excitation, or all excitation timers in the case of the catalyst.
}
