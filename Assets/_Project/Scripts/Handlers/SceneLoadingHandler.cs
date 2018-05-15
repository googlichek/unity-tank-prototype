using UnityEngine;
using UnityEngine.SceneManagement;

namespace TankProto
{
	public class SceneLoadingHandler : MonoBehaviour
	{
		public void LoadScene(int sceneIndex)
		{
			if (sceneIndex < 0 ||
			    sceneIndex > SceneManager.sceneCountInBuildSettings - 1) return;

			SceneManager.LoadScene(sceneIndex);
		}

		public void LoadNextScene()
		{
			var sceneIndex = SceneManager.GetActiveScene().buildIndex;
			if (sceneIndex >= SceneManager.sceneCountInBuildSettings - 1) return;

			sceneIndex++;
			SceneManager.LoadScene(sceneIndex);
		}
	}
}