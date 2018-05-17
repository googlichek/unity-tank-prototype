using System.Collections;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;

namespace TankProto
{
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

		[SerializeField] [Range(0, 1)] private float _projectileScale = 0.1f;
		[SerializeField] [Range(0, 1)] private float _projectileGrowthTime = 0.05f;

		[SerializeField] [Range(0, 1)] private float _weaponChangeDuration = 0.5f;
		[SerializeField] [Range(0, 2)] private float _primaryWeaponFireDelay = 0.25f;
		[SerializeField] [Range(0, 2)] private float _secondaryWeaponFireDelay = 1f;

		[SerializeField] private Ease _changeEase = Ease.Linear;
		[SerializeField] private Ease _scaleEase = Ease.Linear;

		private ProjectileRoot _projectileRoot = null;

		private Weapons _currentWeapon = Weapons.Primary;

		void Start()
		{
			_projectileRoot = FindObjectOfType<ProjectileRoot>();
		}

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

		public void UpdateCurrentWeapon()
		{
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

		public void HandleProjectileLaunching()
		{
			if (DisableInputEvent != null) DisableInputEvent();

			var projectile = SpawnProjectile();
			Launch(projectile);
		}

		private GameObject SpawnProjectile()
		{
			GameObject projectile = null;

			switch (_currentWeapon)
			{
				case Weapons.Primary:
					projectile =
						Instantiate(
							_primaryProjectile,
							_projectileStartPosition.position,
							Quaternion.identity,
							_projectileRoot.transform);
					StartCoroutine(HandleFireRate(_primaryWeaponFireDelay));
					break;
				case Weapons.Secondary:
					projectile =
						Instantiate(
							_secondaryProjectile,
							_projectileStartPosition.position,
							Quaternion.identity,
							_projectileRoot.transform);
					StartCoroutine(HandleFireRate(_secondaryWeaponFireDelay));
					break;
			}

			if (projectile == null) return null;

			projectile.transform
				.DOScale(_projectileScale, _projectileGrowthTime)
				.SetEase(_scaleEase);

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
