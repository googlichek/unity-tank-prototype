using DG.Tweening;
using UnityEngine;

namespace TankProto
{
	public class InputHandler : MonoBehaviour
	{
		[Header("Movement Variables")]
		[SerializeField] [Range(0, 100)] private float _movementSpeed = 0f;
		[SerializeField] [Range(0, 100)] private float _rotationSpeed = 0f;

		[Header("Arsenal Variables")]
		[SerializeField] private GameObject _cylinder = null;
		[SerializeField][Range(0, 1)] private float _changeDuration = 0.5f;
		[SerializeField] private Ease _changeEase = Ease.Linear;

		[Header("Obstacle Detection Variables")]
		[SerializeField] [Range(0, 3)] private float _castHeight = 1;
		[SerializeField] [Range(0, 10)] private float _rayDistance = 4;
		[SerializeField] [Range(0, 1)] private float _sphereRadius = 0.5f;

		private const int LayerMask =
			1 << GlobalVariables.EnvironmentLayer | 1 << GlobalVariables.CharacterLayer;

		private bool _isInputEnabled = true;

		void FixedUpdate()
		{
			HandleMovement();
			HandleRotation();
			ChangeWeapon();
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

			if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q))
			{
				_isInputEnabled = false;

				Vector3 rotationVector =
					_cylinder.transform.localRotation.eulerAngles + new Vector3(0, 0, 180);

				_cylinder.transform
					.DOLocalRotate(rotationVector, _changeDuration)
					.SetEase(_changeEase)
					.OnComplete(() => _isInputEnabled = true);
			}
		}
	}
}
