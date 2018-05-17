using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace TankProto
{
	[RequireComponent(typeof(MovementHandler))]
	[RequireComponent(typeof(WeaponHandler))]
	public class PlayerHandler : MonoBehaviour
	{

		private MovementHandler _movementHandler = null;
		private WeaponHandler _weaponHandler = null;

		private float _velocityOffset = 0;
		private bool _isInputEnabled = true;

		void Start()
		{
			_movementHandler = FindObjectOfType<MovementHandler>();

			_weaponHandler = FindObjectOfType<WeaponHandler>();
			_weaponHandler.DisableInputEvent += DisableInput;
			_weaponHandler.EnableInputEvent += EnableInput;
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
			_velocityOffset = deltaZ;

			_movementHandler.Move(deltaZ);
		}

		private void HandleRotation()
		{
			float deltaY = Input.GetAxis(GlobalVariables.HorizontalAxis);
			_movementHandler.Rotate(deltaY);
		}

		private void ChangeWeapon()
		{
			if (!_isInputEnabled) return;

			if (Input.GetKeyDown(KeyCode.E))
			{
				_weaponHandler.RotateCylinder(180);
				_weaponHandler.UpdateCurrentWeapon();
			}

			if (Input.GetKeyDown(KeyCode.Q))
			{
				_weaponHandler.RotateCylinder(-180);
				_weaponHandler.UpdateCurrentWeapon();
			}
		}

		private void HandleWeapons()
		{
			if (!_isInputEnabled) return;

			if (Input.GetKey(KeyCode.X))
			{
				_weaponHandler.HandleProjectileLaunching(_velocityOffset);
			}
		}

		private void EnableInput()
		{
			_isInputEnabled = true;
		}

		private void DisableInput()
		{
			_isInputEnabled = false;
		}
	}
}
