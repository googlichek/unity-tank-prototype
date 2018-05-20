using DG.Tweening;
using UnityEngine;

namespace TankProto
{
	/// <summary>
	/// Does damage.
	/// </summary>
	[RequireComponent(typeof(Collider))]
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(AudioSource))]
	public class Projectile : MonoBehaviour, IDamageDealer
	{
		public int Damage { get { return _damage; } }

		[Header("Projectile Variables")]
		[SerializeField] [Range(0, 100)] private int _damage = 50;
		[SerializeField] [Range(0, 1)] private float _blowUpDuration = 0.1f;
		[SerializeField] private Ease _blowUpEase = Ease.Linear;

		private AudioSource _audioSource = null;

		void OnEnable()
		{
			_audioSource = GetComponent<AudioSource>();
		}

		void OnTriggerEnter(Collider bumpCollider)
		{
			HandleBlowUp();
		}

		/// <summary>
		/// Starts chain of actions leading to projectile entity destruction.
		/// </summary>
		public void HandleBlowUp()
		{
			_audioSource.Play();

			PrepatreToBlowUp();
			CompleteBlowUp();
		}

		private void PrepatreToBlowUp()
		{
			GetComponent<Rigidbody>().velocity = Vector3.zero;
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
