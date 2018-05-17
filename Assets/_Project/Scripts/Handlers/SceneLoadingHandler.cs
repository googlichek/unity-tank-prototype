using UnityEngine;
using UnityEngine.SceneManagement;

namespace TankProto
{
	/// <summary>
	/// Contains methods for loading scenes.
	/// </summary>
	public class SceneLoadingHandler : MonoBehaviour
	{
		/// <summary>
		/// Loads scene with given index in accordance with build order.
		/// </summary>
		/// <param name="sceneIndex"></param>
		public void LoadScene(int sceneIndex)
		{
			if (sceneIndex < 0 ||
			    sceneIndex > SceneManager.sceneCountInBuildSettings - 1) return;

			SceneManager.LoadScene(sceneIndex);
		}

		/// <summary>
		/// Loads next scene in build order.
		/// </summary>
		public void LoadNextScene()
		{
			var sceneIndex = SceneManager.GetActiveScene().buildIndex;
			if (sceneIndex >= SceneManager.sceneCountInBuildSettings - 1) return;

			sceneIndex++;
			SceneManager.LoadScene(sceneIndex);
		}
	}
}