using UnityEngine;

public class Excitable : MonoBehaviour {
	public float Energy { get; protected set; }

	protected bool depleted = true;

	protected virtual void Update() {
		if (depleted) return;

		Energy -= Time.deltaTime;

		if (Energy < 0) {
			Energy = 0;
			
			Deplete();
		}
    }

	public virtual void Excite(float energy) {
		Energy += energy;
		print("Energy: " + energy);
		depleted = false;
	}

	protected virtual void Deplete() {
		depleted = true;

		print("Depleted");
	}
}
