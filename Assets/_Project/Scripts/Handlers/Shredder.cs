using UnityEngine;

namespace TankProto
{
	public class Shredder : MonoBehaviour
	{
		void OnTriggerEnter(Collider bumpCollider)
		{
			Destroy(bumpCollider.gameObject);
		}
	}
}
