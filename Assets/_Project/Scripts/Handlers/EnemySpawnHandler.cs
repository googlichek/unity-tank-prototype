using System.Collections;
using UnityEngine;

namespace TankProto
{
	public class EnemySpawnHandler : MonoBehaviour
	{
		[Header("Spawning Variables")]
		[SerializeField] private GameObject[] _enemyTypes = null;
		[SerializeField] private Transform[] _enemySpawnPositions = null;
		[SerializeField] private Transform _spawnRoot = null;
		[SerializeField] [Range(0, 20)] private float _enemyAmount = 10;
		[SerializeField] [Range(0, 5)] private float _spawnDelay = 2;

		private bool _canSpawn = true;

		void Update()
		{
			if (!_canSpawn || GameData.EnemiesInAction >= _enemyAmount) return;

			Spawn();
		}

		private void Spawn()
		{
			_canSpawn = false;
			StartCoroutine(HandleSpawnDelay());

			GameData.EnemiesInAction++;

			int enemyIndex = Random.Range(0, _enemyTypes.Length);
			int positionIndex = Random.Range(0, _enemySpawnPositions.Length);

			GameObject enemy =
				Instantiate(
					_enemyTypes[enemyIndex],
					_enemySpawnPositions[positionIndex].position,
					_enemySpawnPositions[positionIndex].rotation,
					_spawnRoot);
		}

		private IEnumerator HandleSpawnDelay()
		{
			yield return new WaitForSeconds(_spawnDelay);
			_canSpawn = true;
		}

	}
}