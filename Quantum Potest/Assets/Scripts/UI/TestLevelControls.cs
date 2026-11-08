using TMPro;
using UnityEngine;

public class TestLevelControls : MonoBehaviour {
	[SerializeField] GameObject renamePanel;
	[SerializeField] TMP_InputField newNameField;
	[SerializeField] TMP_Text title;

	private void Start() {
		LevelDataManager.instance.OnLevelInfoChanged += UpdateTitle;
	}

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
		newNameField.text = LevelDataManager.instance.CurrentLevel.gameObject.name;
	}

	public void RenameConfirm() {
		string name = newNameField.text;

		LevelDataManager.instance.RenameCurrentLevel(name);

		renamePanel.SetActive(false);
	}

	public void AddLevelVersion() {
		LevelDataManager.instance.AddLevelVersion();
	}

	public void CycleLevelVersions() {
		LevelDataManager.instance.CycleLevelVersions();
	}

	public void RemoveLevelVersion() {
		LevelDataManager.instance.RemoveCurrLevelVersion();
	}

	public void Delete() {
		LevelDataManager.instance.RemoveCurrentLevel();
	}

	public void UpdateTitle(int index, string name, int version, int versionCount) {
		title.text = $"{index + 1}. {name}    Ver: {version + 1}/{versionCount}";
	}
}
