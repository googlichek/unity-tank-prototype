using UnityEngine;

namespace TankProto
{
	/// <summary>
	/// Handles bacground music.
	/// </summary>
	public class AudioHandler : MonoBehaviour
	{
		private static AudioHandler _instance = null;

		void Awake()
		{
			HandleExtraHandlers();
		}

		private void HandleExtraHandlers()
		{
			if (_instance != null && _instance != this)
			{
				Destroy(gameObject);
				return;
			}

			_instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}
}
