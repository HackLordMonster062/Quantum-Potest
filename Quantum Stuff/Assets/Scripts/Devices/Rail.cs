using DG.Tweening;
using UnityEngine;

public class Rail : Activatable {
    [SerializeField] Transform device;
    [SerializeField] Transform[] pathPoints;
    [SerializeField] float speed;

    int _currPoint = 0;
    bool _forward = false;
    Tween _currTween;

	private void Start() {
		device.position = pathPoints[0].position;
	}

	public override void Activate() {
        if (_currTween == null || !_currTween.IsActive())
            StartTravel();
	}

	void StartTravel() {
        _forward = !_forward;

        _currPoint = _forward ? 0 : pathPoints.Length - 1;

        MoveToNextPoint();
    }

    void MoveToNextPoint() {
        Vector3 target = pathPoints[_currPoint].position;

		float duration = Vector3.Distance(device.position, target) / speed;

        _currTween = device.DOMove(target, duration)
            .SetEase(Ease.Linear)
            .OnComplete(OnReached);
    }

    void OnReached() {
        _currPoint += _forward ? 1 : -1;

        if (_currPoint < pathPoints.Length && _currPoint >= 0) {
            MoveToNextPoint();
		}
    }
}
