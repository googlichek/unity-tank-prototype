using UnityEngine;
using UnityEngine.UI;

namespace TankProto
{
	/// <summary>
	/// Class for handling UI elements.
	/// </summary>
	public class UIHandler : MonoBehaviour
	{
		private const string DefaultScoreText = "Score:";
		private const string DefaultHealthText = "Health:";
		private const string Percent = "%";

		[SerializeField] private Text _healthCounter = null;
		[SerializeField] private Text _scoreCounter = null;

		private HealthHandler _playerHealth = null;
		private float _maxHealth = 0;

		void Start()
		{
			_playerHealth =
				FindObjectOfType<PlayerHandler>().gameObject.GetComponent<HealthHandler>();
			if (_playerHealth != null) _maxHealth = _playerHealth.HealthPoints;
		}

		void Update()
		{
			UpdateHealthPoints();
			UpdateScorePoints();
		}

		private void UpdateHealthPoints()
		{
			if (_healthCounter == null) return;

			float helthPercentage = _playerHealth.HealthPoints / _maxHealth * 100;
			_healthCounter.text =
				string.Format(
					"{0} {1:0.00} {2}",
					DefaultHealthText,
					helthPercentage,
					Percent);
		}

		private void UpdateScorePoints()
		{
			if (_scoreCounter == null) return;

			_scoreCounter.text =
				string.Format("{0} {1}", DefaultScoreText, GameData.Score);
		}
	}
}
