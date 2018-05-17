using UnityEngine;

namespace TankProto
{
	public class EnemyHandler : MonoBehaviour
	{
		public float Speed = 5f;
		public float ObstacleRange = 20f;
		public float Gravity = -9.8f;

		private GameObject _player = null;
		private bool _alive;
		private bool _playerDetected;


		void Start()
		{
			_player = FindObjectOfType<InputHandler>().gameObject;
			_alive = true;
			_playerDetected = false;
		}

		void Update()
		{
			if (_alive)
			{
				MoveUnit();
			}
		}

		public void SetAlive(bool alive)
		{
			_alive = alive;
		}

		private void MoveUnit()
		{
			if (transform.position.y <= 0.6f && transform.position.y >= 0f)
			{
				transform.Translate(0, 0, Speed * Time.deltaTime);
			}
			else
			{
				transform.Translate(0, Gravity * Time.deltaTime, Speed * Time.deltaTime);
			}

			// Casts ray to detect obstacles within 20 world units by default.
			Ray forwardRay = new Ray(transform.position, transform.forward);
			RaycastHit hit;

			if (Physics.SphereCast(forwardRay, 0.75f, out hit))
			{
				if (_player == null)
				{
					_playerDetected = false;
				}
				else if (_playerDetected)
				{
					transform.LookAt(
						new Vector3(
							_player.transform.position.x,
							transform.position.y,
							_player.transform.position.z));
				}
				else if (hit.transform.position == _player.transform.position)
				{
					_playerDetected = true;
				}
				else if (hit.distance < ObstacleRange)
				{
					float angle = Random.Range(-120, 120);
					transform.Rotate(0, angle, 0);
				}
			}
		}
	}
}
