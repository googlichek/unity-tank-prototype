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
			Projectile projectile = bumpCollider.GetComponent<Projectile>();
			if (projectile == null) return;

			projectile.BlowUp();
		}
	}
}
