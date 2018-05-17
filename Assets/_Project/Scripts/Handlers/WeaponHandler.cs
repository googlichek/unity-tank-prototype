using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace TankProto
{
	/// <summary>
	/// Contains methods for handling weapons in player's posession.
	/// </summary>
	[RequireComponent(typeof(AudioSource))]
	public class WeaponHandler : MonoBehaviour
	{
		public delegate void OnDisableInput();
		public event OnDisableInput DisableInputEvent;

		public delegate void OnEnableInput();
		public event OnEnableInput EnableInputEvent;

		[Header("Arsenal Variables")]
		[SerializeField] private GameObject _cylinder = null;
		[SerializeField] private GameObject _primaryProjectile = null;
		[SerializeField] private GameObject _secondaryProjectile = null;
		[SerializeField] private Transform _projectileStartPosition = null;

		[SerializeField] [Range(0, 1000)] private int _launchForce = 500;

		[SerializeField] [Range(0, 1)] private float _primaryProjectileScale = 0.1f;
		[SerializeField] [Range(0, 1)] private float _secondaryProjectileScale = 0.25f;
		[SerializeField] [Range(0, 1)] private float _projectileGrowthTime = 0.05f;

		[SerializeField] [Range(0, 1)] private float _weaponChangeDuration = 0.5f;
		[SerializeField] [Range(0, 2)] private float _primaryWeaponFireDelay = 0.25f;
		[SerializeField] [Range(0, 2)] private float _secondaryWeaponFireDelay = 1f;

		[SerializeField] private Ease _changeEase = Ease.Linear;
		[SerializeField] private Ease _scaleEase = Ease.Linear;

		[Header("Sounds")]
		[SerializeField] private AudioClip _weaponChange = null;
		[SerializeField] private AudioClip _primaryShot = null;
		[SerializeField] private AudioClip _secondaryShot = null;

		private AudioSource _audioSource = null;
		private ProjectileRoot _projectileRoot = null;

		private Weapons _currentWeapon = Weapons.Primary;

		void Start()
		{
			_audioSource = GetComponent<AudioSource>();
			_projectileRoot = FindObjectOfType<ProjectileRoot>();
		}

		/// <summary>
		/// Animates weapon change.
		/// </summary>
		/// <param name="value">Rotation value for firearms cylinder.</param>
		public void RotateCylinder(float value)
		{
			if (DisableInputEvent != null) DisableInputEvent();

			Vector3 rotationVector =
				_cylinder.transform.localRotation.eulerAngles + new Vector3(0, 0, value);

			_cylinder.transform
				.DOLocalRotate(rotationVector, _weaponChangeDuration)
				.SetEase(_changeEase)
				.OnComplete(() =>
				{
					if (EnableInputEvent != null) EnableInputEvent();
				});
		}

		/// <summary>
		/// Updates player's active weapon.
		/// </summary>
		public void UpdateCurrentWeapon()
		{
			PlayClip(_weaponChange);

			switch (_currentWeapon)
			{
				case Weapons.Primary:
					_currentWeapon = Weapons.Secondary;
					break;
				case Weapons.Secondary:
					_currentWeapon = Weapons.Primary;
					break;
			}
		}

		/// <summary>
		/// Creates & launches projectile.
		/// </summary>
		public void HandleProjectileLaunching()
		{
			if (DisableInputEvent != null) DisableInputEvent();

			var projectile = SpawnProjectile();
			Launch(projectile);
		}

		private void PlayClip(AudioClip clip)
		{
			_audioSource.Pause();
			_audioSource.clip = clip;
			_audioSource.Play();
		}

		private GameObject SpawnProjectile()
		{
			GameObject projectile = null;

			switch (_currentWeapon)
			{
				case Weapons.Primary:
					StartCoroutine(HandleFireRate(_primaryWeaponFireDelay));

					projectile =
						Instantiate(
							_primaryProjectile,
							_projectileStartPosition.position,
							Quaternion.identity,
							_projectileRoot.transform);

					projectile.transform
						.DOScale(_primaryProjectileScale, _projectileGrowthTime)
						.SetEase(_scaleEase);

					PlayClip(_primaryShot);

					break;
				case Weapons.Secondary:
					StartCoroutine(HandleFireRate(_secondaryWeaponFireDelay));

					projectile =
						Instantiate(
							_secondaryProjectile,
							_projectileStartPosition.position,
							Quaternion.identity,
							_projectileRoot.transform);

					projectile.transform
						.DOScale(_secondaryProjectileScale, _projectileGrowthTime)
						.SetEase(_scaleEase);

					PlayClip(_secondaryShot);

					break;
			}

			return projectile;
		}

		private void Launch(GameObject projectile)
		{
			Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
			projectileRigidbody.AddForce(transform.forward * _launchForce);
		}

		private IEnumerator HandleFireRate(float delay)
		{
			yield return new WaitForSeconds(delay);
			if (EnableInputEvent != null) EnableInputEvent();
		}
	}
}
