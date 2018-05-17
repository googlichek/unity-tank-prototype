using DG.Tweening;
using UnityEngine;

namespace TankProto
{
	public class MovementHandler : MonoBehaviour
	{
		public delegate void OnDisableMovement();
		public event OnDisableMovement DisableMovementEvent;

		public delegate void OnEnableMovement();
		public event OnEnableMovement EnableMovementEvent;

		private const float MandatoryRotationAngle = 180;

		[Header("Movement Variables")]
		[SerializeField] [Range(0, 100)] private float _movementSpeed = 0f;
		[SerializeField] [Range(0, 100)] private float _rotationSpeed = 0f;

		[Header("Obstacle Detection Variables")]
		[SerializeField] [Range(0, 3)] private float _castHeight = 1;
		[SerializeField] [Range(0, 10)] private float _rayDistance = 4;
		[SerializeField] [Range(0, 1)] private float _sphereRadius = 0.5f;

		[Header("Direction Change Variables")]
		[SerializeField] [Range(0, 2)] private float _autoMovementSpeed = 1f;
		[SerializeField] [Range(-180, 180)] private float _maxRotationRandomizer = 120f;
		[SerializeField] [Range(-180, 180)] private float _minRotationRandomizer = -120f;
		[SerializeField] [Range(0, 3)] private float _rotationDuration = 0.5f;

		private const int LayerMask =
			1 << GlobalVariables.EnvironmentLayer | 1 << GlobalVariables.CharacterLayer;

		public void Move(float deltaZ)
		{
			Vector3 offset = new Vector3(0, 0, deltaZ * _movementSpeed) * Time.deltaTime;

			if (offset == Vector3.zero) return;
			if (!CheckIfMovementIsPossible(deltaZ))
			{
				//ChangeDirection();
				return;
			}

			transform.Translate(offset);
		}

		public void Rotate(float deltaY)
		{
			Vector3 offset = new Vector3(0f, deltaY * _rotationSpeed, 0f) * Time.deltaTime;

			if (offset == Vector3.zero) return;
			transform.Rotate(offset);
		}

		//private void ChangeDirection()
		//{
		//	if (DisableMovementEvent == null) return;

		//	DisableMovementEvent();

		//	float rotationAngle =
		//		Random.Range(_minRotationRandomizer, _maxRotationRandomizer) +
		//		transform.rotation.eulerAngles.y +
		//		MandatoryRotationAngle;

		//	Vector3 rotationVector = new Vector3(0, rotationAngle, 0);

		//	transform
		//		.DORotate(rotationVector, _rotationDuration)
		//		.OnComplete(() =>
		//		{
		//			if (EnableMovementEvent != null) EnableMovementEvent();
		//		});
		//}

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
	}
}
