using UnityEngine;

namespace TankProto
{
	[RequireComponent(typeof(MovementHandler))]
	[RequireComponent(typeof(WeaponHandler))]
	public class PlayerHandler : MonoBehaviour
	{
		private const int CylinderRotationAngle = 180;

		private MovementHandler _movementHandler = null;
		private WeaponHandler _weaponHandler = null;

		private bool _isInputEnabled = true;

		void Start()
		{
			GameData.Score = 0;

			_movementHandler = GetComponent<MovementHandler>();

			_weaponHandler = GetComponent<WeaponHandler>();
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
				_weaponHandler.RotateCylinder(CylinderRotationAngle);
				_weaponHandler.UpdateCurrentWeapon();
			}

			if (Input.GetKeyDown(KeyCode.Q))
			{
				_weaponHandler.RotateCylinder(-CylinderRotationAngle);
				_weaponHandler.UpdateCurrentWeapon();
			}
		}

		private void HandleWeapons()
		{
			if (!_isInputEnabled) return;

			if (Input.GetKey(KeyCode.X))
			{
				_weaponHandler.HandleProjectileLaunching();
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
