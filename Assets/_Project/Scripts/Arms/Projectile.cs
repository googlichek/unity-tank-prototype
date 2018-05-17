using DG.Tweening;
using UnityEngine;

namespace TankProto
{
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
		private Rigidbody _rigidbody = null;

		void OnEnable()
		{
			_audioSource = GetComponent<AudioSource>();
			_rigidbody = GetComponent<Rigidbody>();
		}

		public void BlowUp()
		{
			_audioSource.Play();

			switch (MasterEntity)
			{
				case MasterEntity.Player:
					_rigidbody.velocity = Vector3.zero;
					CompleteBlowUp();
					break;
				case MasterEntity.Enemy:
					GameData.EnemiesInAction--;
					GameData.Score += _scoreValue;
					GetComponent<MovementHandler>().FireMovementDisablingEvent();
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
