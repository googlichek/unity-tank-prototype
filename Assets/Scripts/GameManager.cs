using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private GameManager _instance;

	void Awake()
	{
		HandleGameManagerInitialization();
	}

	public void LoadScene(int sceneIndex)
	{
		SceneManager.LoadScene(sceneIndex);
	}

	public void LoadNextScene()
	{
		int sceneIndex = SceneManager.GetActiveScene().buildIndex;
		if (sceneIndex >= SceneManager.sceneCountInBuildSettings - 1) return;

		sceneIndex++;
		SceneManager.LoadScene(sceneIndex);
	}

	private void HandleGameManagerInitialization()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			_instance = this;
		}
	}
}
