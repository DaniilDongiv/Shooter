using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Script
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private Transform _player;
        [SerializeField]
        private EnemySpawn _enemy;
        [SerializeField]
        private NavMeshAgent _navMeshAgent;
        [SerializeField] 
        private PlayerController _playerController;
        [SerializeField]
        private GlobalUIManager _globalUIManager;

        public Rigidbody[] Rigidbodies;
        private bool _isDeath = true;
        private Animator _animator;
        private static readonly int _hit = Animator.StringToHash("hit");
        
        void Start()
        {
            foreach(var rigidbody in Rigidbodies)
            {
                rigidbody.isKinematic = true;
            }
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _globalUIManager.QuantityEnemyText.text = _enemy.QuantityEnemy.ToString();
        }
    
        public void ShowDeath()
        {
            foreach(var rigidbody in GetComponentsInChildren<Rigidbody>())
            {
                rigidbody.isKinematic = false;
            }
            gameObject.GetComponentInParent<Animator>().enabled = false;
            if (_isDeath)
            {
                _enemy.QuantityEnemy--;
                _globalUIManager.QuantityEnemyText.text = _enemy.QuantityEnemy.ToString();
            }
            _isDeath = false;
            GetComponent<NavMeshAgent>().enabled = false;
        }
        
        void Update()
        {
            if (_isDeath)
            {
                transform.LookAt(_player);
                _navMeshAgent.SetDestination(_player.transform.position);
            }
        }
        
        public void AnimatorHit()
        {
            if (_isDeath)
            {
                StartCoroutine(Pause(3.5f));
                _animator.SetTrigger(_hit);
                _playerController._hp --;
                _globalUIManager._hpText.text = _playerController._hp.ToString();
            }
        }

        private IEnumerator Pause(float second)
        {
            _navMeshAgent.speed = 0f;
            yield return new WaitForSeconds(second);
            _navMeshAgent.speed = 3.5f;
        }
    }
}