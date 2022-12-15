using UnityEngine;
using Random = UnityEngine.Random;

namespace Script
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private int _quantityEnemy;
        [SerializeField]
        private GameObject _enemy;

        private void Start()
        {
            EventManager.EnemyDead += OnSpawnerEnemy;
            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            for (int i = 0; i < _quantityEnemy; i++)
            {
                var createdEnemyPositionX = Random.Range(-88, 80);
                var createdEnemyPositionZ = Random.Range(70, 86);
                var enemy = Instantiate(_enemy);
                enemy.transform.position = new Vector3(createdEnemyPositionX, 0.168f, createdEnemyPositionZ);
            }
        }

        private void OnSpawnerEnemy()
        {
            var createdEnemyPositionX = Random.Range(-88, 80);
            var createdEnemyPositionZ = Random.Range(70, 86);
            var enemy = Instantiate(_enemy);
            enemy.transform.position = new Vector3(createdEnemyPositionX, 0.168f, createdEnemyPositionZ);
        }
    }
}