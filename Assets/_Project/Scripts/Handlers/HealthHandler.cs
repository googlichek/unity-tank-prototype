using UnityEngine;

namespace TankProto
{
	[RequireComponent(typeof(Collider))]
	public class HealthHandler : MonoBehaviour
	{
		[Header("Health Variables")]
		[SerializeField] [Range(0, 1000)] private float _healthPoints = 150;
		[SerializeField] [Range(0, 1)] private float _armor = 0.5f;
		[SerializeField] [Range(0, 1000)] private int _scoreValue = 100;

		void OnTriggerEnter(Collider bumpCollider)
		{
			Projectile projectile = bumpCollider.GetComponent<Projectile>();
			if (projectile == null) return;

			_healthPoints -= (1 - _armor) * projectile.Damage;
			projectile.BlowUp();
			if (_healthPoints <= 0) HandleDefeat();
		}

		private void HandleDefeat()
		{
		}
	}
}
