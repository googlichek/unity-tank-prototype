using UnityEngine;
using UnityEngine.UI;

namespace TankProto
{
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