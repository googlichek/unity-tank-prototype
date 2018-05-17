using UnityEngine;

namespace TankProto
{
	public class EnemySpawnHandler : MonoBehaviour
	{
		[Header("Spawning Variables")]
		[SerializeField] private GameObject[] _enemyTypes = null;
		[SerializeField] private Transform[] _enemySpawnPositions = null;
	}
}