using UnityEngine;
using Random = System.Random;

namespace Script
{
    public class EnemySpawn : MonoBehaviour
    {
        [SerializeField]
        private GameObject _enemy;
        
        public int QuantityEnemy;

        void Start()
        {
            var randomStartEnemy = new Random();
            var posX = randomStartEnemy.Next(-88, 80);
            var posZ = randomStartEnemy.Next(50, 86);
            _enemy.transform.position = new Vector3(posX, 0.168f, posZ);
        
            for (int i = 0; i < QuantityEnemy-1; i++)
            {
                var random = new Random();
                var rndX = random.Next(-88, 80);
                var rndZ = random.Next(50, 87);
                var enemy = Instantiate(_enemy);
                enemy.transform.position = new Vector3(rndX, 0.168f, rndZ);
            }
        }
    }
}