using UnityEngine;
using UnityEngine.UI;

namespace TankProto
{
	/// <summary>
	/// Helps with displaying user score.
	/// </summary>
	public class ScoreHandler : MonoBehaviour
	{
		private const string DefaultText = "Score: ";

		[SerializeField] private Text _scoreCounter = null;

		void Update()
		{
			_scoreCounter.text = DefaultText + GameData.Score;
		}
	}
}
