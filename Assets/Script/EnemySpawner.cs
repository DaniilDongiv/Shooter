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
            var firstEnemyPositionXCreated = Random.Range(-88, 80);
            var firstEnemyPositionZCreated = Random.Range(50, 86);
            _enemy.transform.position = new Vector3(firstEnemyPositionXCreated, 0.168f, firstEnemyPositionZCreated);
            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            for (int i = 0; i < _quantityEnemy-1; i++)
            {
                var createdEnemyPositionX = Random.Range(-88, 80);
                var createdEnemyPositionZ = Random.Range(50, 86);
                var enemy = Instantiate(_enemy);
                enemy.transform.position = new Vector3(createdEnemyPositionX, 0.168f, createdEnemyPositionZ);
            }
        }
        
        public int QuantityEnemy()
        {
            return _quantityEnemy;
        }
    }
}