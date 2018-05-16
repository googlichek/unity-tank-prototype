using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace TankProto
{
	public class PlayerHandler : MonoBehaviour
	{
		private enum Weapons
		{
			Primary,
			Secondary
		}

		[Header("Movement Variables")]
		[SerializeField] [Range(0, 100)] private float _movementSpeed = 0f;
		[SerializeField] [Range(0, 100)] private float _rotationSpeed = 0f;

		[Header("Arsenal Variables")]
		[SerializeField] private GameObject _cylinder = null;
		[SerializeField] private GameObject _primaryProjectile = null;
		[SerializeField] private GameObject _secondaryProjectile = null;
		[SerializeField] private Transform _projectileStartPosition = null;

		[SerializeField] [Range(0, 1)] private float _projectileScale = 0.1f;
		[SerializeField] [Range(0, 1)] private float _projectileGrowthTime = 0.05f;
		[SerializeField] [Range(0, 1)] private float _projectileTravelTime = 1f;

		[SerializeField] [Range(0, 1)] private float _weaponChangeDuration = 0.5f;
		[SerializeField] [Range(0, 2)] private float _primaryWeaponFireDelay = 0.25f;
		[SerializeField] [Range(0, 2)] private float _secondaryWeaponFireDelay = 1f;

		[SerializeField] private Ease _changeEase = Ease.Linear;
		[SerializeField] private Ease _scaleEase = Ease.Linear;

		[Header("Obstacle Detection Variables")]
		[SerializeField] [Range(0, 3)] private float _castHeight = 1;
		[SerializeField] [Range(0, 10)] private float _rayDistance = 4;
		[SerializeField] [Range(0, 1)] private float _sphereRadius = 0.5f;

		private const int LayerMask =
			1 << GlobalVariables.EnvironmentLayer | 1 << GlobalVariables.CharacterLayer;

		private ProjectileRoot _projectileRoot = null;

		private Weapons _currentWeapon = Weapons.Primary;

		private bool _isInputEnabled = true;

		void Start()
		{
			_projectileRoot = FindObjectOfType<ProjectileRoot>();
		}

		void FixedUpdate()
		{
			HandleMovement();
			HandleRotation();
			ChangeWeapon();
			HandleWeapons();
		}

		private void HandleMovement()
		{
			float deltaZ = Input.GetAxis(GlobalVariables.VerticalAxis);
			Vector3 offset = new Vector3(0, 0, deltaZ * _movementSpeed);

			if (offset == Vector3.zero) return;
			if (!CheckIfMovementIsPossible(deltaZ)) return;

			transform.Translate(offset * Time.deltaTime);
		}

		private bool CheckIfMovementIsPossible(float deltaZ)
		{
			Vector3 rayOrigin =
				new Vector3(transform.position.x, _castHeight, transform.position.z);

			RaycastHit hitTarget;

			Ray ray =
				deltaZ < 0 ?
					new Ray(rayOrigin, -transform.forward) :
					new Ray(rayOrigin, transform.forward);

			bool isHit =
				Physics.SphereCast(ray, _sphereRadius, out hitTarget, _rayDistance, LayerMask);
			if (isHit && hitTarget.distance <= _rayDistance) return false;

			return true;
		}

		private void HandleRotation()
		{
			float deltaY = Input.GetAxis(GlobalVariables.HorizontalAxis);
			Vector3 offset = new Vector3(0f, deltaY * _rotationSpeed, 0f);

			transform.Rotate(offset * Time.deltaTime);
		}

		private void ChangeWeapon()
		{
			if (!_isInputEnabled) return;

			if (Input.GetKeyDown(KeyCode.E))
			{
				RotateCylinder(180);
				UpdateCurrentWeapon();
			}

			if (Input.GetKeyDown(KeyCode.Q))
			{
				RotateCylinder(-180);
				UpdateCurrentWeapon();
			}
		}

		private void RotateCylinder(float value)
		{
			_isInputEnabled = false;

			Vector3 rotationVector =
				_cylinder.transform.localRotation.eulerAngles + new Vector3(0, 0, value);

			_cylinder.transform
				.DOLocalRotate(rotationVector, _weaponChangeDuration)
				.SetEase(_changeEase)
				.OnComplete(() => _isInputEnabled = true);
		}

		private void UpdateCurrentWeapon()
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

		private void HandleWeapons()
		{
			if (!_isInputEnabled) return;

			if (Input.GetKey(KeyCode.X)) Fire();
		}

		private void Fire()
		{
			_isInputEnabled = false;

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

			if (projectile == null) return;

			projectile.transform
				.DOScale(_projectileScale, _projectileGrowthTime)
				.SetEase(_scaleEase);
		}

		private IEnumerator HandleFireRate(float delay)
		{
			yield return new WaitForSeconds(delay);
			_isInputEnabled = true;
		}
	}
}
