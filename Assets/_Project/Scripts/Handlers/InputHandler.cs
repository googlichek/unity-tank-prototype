using UnityEngine;

namespace TankProto
{
	public class InputHandler : MonoBehaviour
	{
		[Header("Movement Variables")]
		[SerializeField] [Range(0, 100)] private float _movementSpeed = 0f;
		[SerializeField] [Range(0, 100)] private float _rotationSpeed = 0f;

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