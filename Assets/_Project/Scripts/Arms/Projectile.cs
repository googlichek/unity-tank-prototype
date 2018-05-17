using UnityEngine;

namespace TankProto
{
	public class Projectile : MonoBehaviour
	{
		[Header("Projectile Variables")]
		[Range(0, 100)] public int Damage = 50;
		public ProjectileMaster Master = ProjectileMaster.None;

		public void BlowUp()
		{
			Destroy(gameObject);
		}
	}
}