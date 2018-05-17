using UnityEngine;

namespace TankProto
{
	public class MovementHandler : MonoBehaviour
	{
		public delegate void OnObstacleFound();
		public event OnObstacleFound ObstacleFoundEvent;

		[Header("Movement Variables")]
		[SerializeField] [Range(0, 100)] private float _movementSpeed = 0f;
		[SerializeField] [Range(0, 100)] private float _rotationSpeed = 0f;

		[Header("Obstacle Detection Variables")]
		[SerializeField] [Range(0, 3)] private float _castHeight = 1;
		[SerializeField] [Range(0, 10)] private float _rayDistance = 4;
		[SerializeField] [Range(0, 1)] private float _sphereRadius = 0.5f;

		private const int LayerMask =
			1 << GlobalVariables.EnvironmentLayer | 1 << GlobalVariables.CharacterLayer;

		public void Move(float deltaZ)
		{
			Vector3 offset = new Vector3(0, 0, deltaZ * _movementSpeed) * Time.deltaTime;

			if (offset == Vector3.zero) return;
			if (!CheckIfMovementIsPossible(deltaZ)) return;

			transform.Translate(offset);
		}

		public void Rotate(float deltaY)
		{
			Vector3 offset = new Vector3(0f, deltaY * _rotationSpeed, 0f) * Time.deltaTime;

			if (offset == Vector3.zero) return;
			transform.Rotate(offset);
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
			if (isHit && hitTarget.distance <= _rayDistance)
			{
				if (ObstacleFoundEvent != null) ObstacleFoundEvent();
				return false;
			}

			return true;
		}
	}
}
