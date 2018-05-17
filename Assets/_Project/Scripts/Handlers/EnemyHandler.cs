using DG.Tweening;
using UnityEngine;

namespace TankProto
{
	[RequireComponent(typeof(MovementHandler))]
	public class EnemyHandler : MonoBehaviour
	{
		private const float MandatoryRotationAngle = 180;

		[SerializeField] [Range(0, 2)] private float _movementSpeed = 1f;
		[SerializeField] [Range(-180, 180)] private float _maxRotationRandomizer = 120f;
		[SerializeField] [Range(-180, 180)] private float _minRotationRandomizer = -120f;
		[SerializeField] [Range(0, 3)] private float _rotationDuration = 1;

		private MovementHandler _movementHandler = null;
		private bool _canMove = true;

		void Start()
		{
			_movementHandler = GetComponent<MovementHandler>();
			_movementHandler.ObstacleFoundEvent += ChangeDirection;
		}

		void FixedUpdate()
		{
			if (!_canMove) return;
			_movementHandler.Move(_movementSpeed);
		}

		private void ChangeDirection()
		{
			_canMove = false;

			float rotationAngle =
				Random.Range(_minRotationRandomizer, _maxRotationRandomizer) +
				transform.rotation.eulerAngles.y +
				MandatoryRotationAngle;

			Vector3 rotationVector = new Vector3(0, rotationAngle, 0);
			Debug.Log(rotationVector);

			transform
				.DORotate(rotationVector, _rotationDuration)
				.OnComplete(() => _canMove = true);
		}
	}
}
