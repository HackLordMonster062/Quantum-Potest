using System;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
	public static event Action<GameState> OnBeforeStateChange;
	public static event Action<GameState> OnAfterStateChange;

	public GameState State { get; private set; }

	public Transform Player { get; private set; }

	[SerializeField] float gravity;
	public float Gravity { get { return gravity; } }

	[SerializeField] float speedOfLight;
	public float SpeedOfLight {  get { return speedOfLight; } }

	void Start() {
		ChangeState(GameState.Initiating);
		Player = GameObject.FindGameObjectWithTag("Player").transform;

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public void ChangeState(GameState newState) {
		OnBeforeStateChange?.Invoke(newState);

		State = newState;
		switch (newState) {
			case GameState.Initiating:
				break;
			case GameState.SpawningRobots:
				break;
			case GameState.EnablingRobots:
				break;
			case GameState.Fighting:
				break;
			case GameState.LevelFinish:
				break;
			case GameState.Death:
				break;
		}

		OnAfterStateChange?.Invoke(newState);
	}
}

[Serializable]
public enum GameState {
	Initiating = 0,
	SpawningRobots = 1,
	EnablingRobots = 2,
	Fighting = 3,
	LevelFinish = 4,
	Death = 5
}