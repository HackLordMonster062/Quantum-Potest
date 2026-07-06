using TMPro;
using UnityEngine;

public class TestLevelControls : MonoBehaviour {
	[SerializeField] GameObject renamePanel;
	[SerializeField] TMP_InputField newNameField;

	public void Save() {
		LevelDataManager.instance.ScrapeLevels();
		LevelDataManager.instance.SaveLevels();
	}

	public void ReloadData() {
		LevelDataManager.instance.ReconstructLevels();
	}

	public void ReloadFiles() {
		LevelDataManager.instance.ReloadLevels();
	}

	public void NewLevel() {
		LevelDataManager.instance.NewLevelAfterCurrent();
	}

	public void DuplicateLevel() {
		LevelDataManager.instance.DuplicateCurrentLevel();
	}

	public void MoveForwards() {
		LevelDataManager.instance.MoveCurrentLevel(LevelDataManager.instance.CurrentLevelIndex + 1);
	}

	public void MoveBackwards() {
		LevelDataManager.instance.MoveCurrentLevel(LevelDataManager.instance.CurrentLevelIndex - 1);
	}

	public void Rename() {
		renamePanel.SetActive(true);
	}

	public void RenameConfirm() {
		string name = newNameField.text;

		LevelDataManager.instance.RenameCurrentLevel(name);

		renamePanel.SetActive(false);
	}

	public void Delete() {
		LevelDataManager.instance.RemoveCurrentLevel();
	}
}
