using UnityEngine;

namespace TankProto
{
	public class InputHandler : MonoBehaviour
	{
		[Header("Movement Variables")]
		[SerializeField] [Range(0, 100)] private float _movementSpeed = 0f;
		[SerializeField] [Range(0, 100)] private float _rotationSpeed = 0f;

		[Header("Obstacle Detection Variables")]
		[SerializeField] [Range(0, 3)] private float _castHeight = 1;
		[SerializeField] [Range(0, 10)] private float _rayDistance = 4;
		[SerializeField] [Range(0, 1)] private float _sphereRadius = 0.5f;

		private const int LayerMask =
			1 << GlobalVariables.EnvironmentLayer | 1 << GlobalVariables.CharacterLayer;

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

			//Vector3 rayOrigin =
			//	new Vector3(transform.position.x, _castHeight, transform.position.z);

			//RaycastHit hitTarget;

			//Ray ray =
			//	deltaZ < 0 ?
			//		new Ray(rayOrigin, -transform.forward) :
			//		new Ray(rayOrigin, transform.forward);

			//bool isHit =
			//	Physics.SphereCast(ray, _sphereRadius, out hitTarget, _rayDistance, LayerMask);
			//if (isHit && hitTarget.distance <= _rayDistance) return;

			transform.Translate(offset * Time.deltaTime);
		}

		private void HandleRotation()
		{
			float deltaY = Input.GetAxis(GlobalVariables.HorizontalAxis);
			Vector3 offset = new Vector3(0f, deltaY * _rotationSpeed, 0f);

			transform.Rotate(offset * Time.deltaTime);
		}

		private void ChangeWeapon()
		{
		}
	}
}
