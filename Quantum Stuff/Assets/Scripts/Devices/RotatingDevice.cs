public class RotatingDevice : Anchor {

	public override void Activate() {
		if (_particle != null && _particle.TryGetComponent(out IRotateable particle)) {
			particle.Rotate();
		}
	}
}
