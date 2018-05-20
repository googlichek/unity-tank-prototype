using UnityEngine;

namespace TankProto
{
	/// <summary>
	/// Destroys projectiles that are trying to escape the boundaries of arena.
	/// </summary>
	public class Shredder : MonoBehaviour
	{
		void OnTriggerEnter(Collider bumpCollider)
		{
			IDamageDealer damageDealer = bumpCollider.GetComponent<Projectile>();
			if (damageDealer == null) return;

			damageDealer.HandleBlowUp();
		}
	}
}
