using UnityEngine;

namespace TankProto
{
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
