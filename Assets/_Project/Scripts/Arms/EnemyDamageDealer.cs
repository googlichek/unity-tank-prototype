using DG.Tweening;
using UnityEngine;

namespace TankProto
{
	/// <summary>
	/// Does damage to player.
	/// </summary>
	[RequireComponent(typeof(Collider))]
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(AudioSource))]
	[RequireComponent(typeof(HealthHandler))]
	public class EnemyDamageDealer : MonoBehaviour, IDamageDealer
	{
		public int Damage { get { return _damage; } }

		[Header("Damage Dealer Variables")]
		[SerializeField] [Range(0, 100)] private int _damage = 50;
		[SerializeField] [Range(0, 1000)] private int _scoreValue = 100;
		[SerializeField] [Range(0, 1)] private float _blowUpDuration = 0.1f;
		[SerializeField] private Ease _blowUpEase = Ease.Linear;

		private AudioSource _audioSource = null;
		private HealthHandler _healthHandler = null;

		void OnEnable()
		{
			_audioSource = GetComponent<AudioSource>();

			_healthHandler = GetComponent<HealthHandler>();
			_healthHandler.TriggerZeroHealthEvent += HandleBlowUp;
		}

		/// <summary>
		/// Starts chain of actions leading to damage dealer entity destruction.
		/// </summary>
		public void HandleBlowUp()
		{
			_audioSource.Play();

			PrepatreToBlowUp();
			CompleteBlowUp();
		}

		private void PrepatreToBlowUp()
		{
			GameData.EnemiesInAction--;
			GameData.Score += _scoreValue;

			GetComponent<MovementHandler>().enabled = false;
		}

		private void CompleteBlowUp()
		{
			transform.DOScale(0, 0);
			transform
				.DOPunchScale(2 * Vector3.one, _blowUpDuration)
				.SetEase(_blowUpEase)
				.OnComplete(() => Destroy(gameObject));
		}
	}
}
