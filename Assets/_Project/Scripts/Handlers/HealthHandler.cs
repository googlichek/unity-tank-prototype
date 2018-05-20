using UnityEngine;

namespace TankProto
{
	/// <summary>
	/// Class for handling player/enemy health.
	/// </summary>
	[RequireComponent(typeof(Collider))]
	public class HealthHandler : MonoBehaviour
	{
		public delegate void OnHealthZeroed();
		public event OnHealthZeroed TriggerZeroHealthEvent;

		[Header("Health Variables")]
		[SerializeField] [Range(0, 1000)] private float _healthPoints = 150;
		[SerializeField] [Range(0, 1)] private float _armor = 0.5f;

		void OnTriggerEnter(Collider bumpCollider)
		{
			IDamageDealer damageDealer = bumpCollider.GetComponent<IDamageDealer>();
			if (damageDealer == null) return;

			HandleHealthPoints(damageDealer.Damage);
		}

		private void HandleHealthPoints(int damage)
		{
			_healthPoints -= (1 - _armor) * damage;
			if (_healthPoints <= 0) HandleDefeat();
		}

		private void HandleDefeat()
		{
			if (TriggerZeroHealthEvent != null) TriggerZeroHealthEvent();
		}
	}
}
