using DG.Tweening;
using UnityEngine;

namespace TankProto
{
	/// <summary>
	/// Does damage.
	/// </summary>
	[RequireComponent(typeof(AudioSource))]
	public class Projectile : MonoBehaviour
	{
		[Header("Projectile Variables")]
		[Range(0, 100)] public int Damage = 50;
		public MasterEntity MasterEntity = MasterEntity.None;
		[SerializeField] [Range(0, 1)] private float _blowUpDuration = 0.1f;
		[SerializeField] [Range(0, 1)] private Ease _blowUpEase = Ease.Linear;
		[SerializeField] [Range(0, 1000)] private int _scoreValue = 100;

		private AudioSource _audioSource = null;

		void OnEnable()
		{
			_audioSource = GetComponent<AudioSource>();
		}

		public void HandleBlowUp()
		{
			_audioSource.Play();

			switch (MasterEntity)
			{
				case MasterEntity.Player:
					GetComponent<Rigidbody>().velocity = Vector3.zero;
					CompleteBlowUp();
					break;
				case MasterEntity.Enemy:
					GameData.EnemiesInAction--;
					GetComponent<MovementHandler>().enabled = false;
					GameData.Score += _scoreValue;
					CompleteBlowUp();
					break;
			}
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
