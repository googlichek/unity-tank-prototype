using UnityEngine;

namespace TankProto
{
	[RequireComponent(typeof(MovementHandler))]
	public class EnemyHandler : MonoBehaviour
	{
		[SerializeField] [Range(0, 2)] private float _movementSpeed = 1f;
		[SerializeField] [Range(-180, 180)] private float _maxRotationRandomizer = 120f;
		[SerializeField] [Range(-180, 180)] private float _minRotationRandomizer = -120f;
		[SerializeField] [Range(0, 3)] private float _rotationDuration = 1;

		private MovementHandler _movementHandler = null;
		private bool _canMove = true;

		void Start()
		{
			_movementHandler = GetComponent<MovementHandler>();
			_movementHandler.DisableMovementEvent += DisableMovement;
			_movementHandler.EnableMovementEvent += EnableMovement;
		}

		void FixedUpdate()
		{
			if (!_canMove) return;
			_movementHandler.Move(_movementSpeed);
		}

		private void DisableMovement()
		{
			_canMove = false;
		}

		private void EnableMovement()
		{
			_canMove = true;
		}
	}
}
