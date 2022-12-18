using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Script
{
    public class EnemyController : MonoBehaviour
    {
        public Rigidbody[] Rigidbodies;
        
        private GameObject _playerPosition;
        private GlobalValuesManager _globalValuesManager;
        private NavMeshAgent _navMeshAgent;
        private PlayerController _playerController;
        private GlobalUIManager _globalUIManager;
        private bool _isDead;
        private Animator _animator;
        private static readonly int _hit = Animator.StringToHash("hit");
        
        private void Start()
        {
            _playerPosition = GameObject.Find("Player");
            _globalValuesManager = GameObject.Find("GlobalValueManager").GetComponent<GlobalValuesManager>();
            _playerController = GameObject.Find("Player").GetComponent<PlayerController>(); 
            _globalUIManager = GameObject.Find("UIManager").GetComponent<GlobalUIManager>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _globalUIManager.QuantityEnemyText.text = _globalValuesManager.QuantityEnemy.ToString();
            
            foreach(var rigidbody in Rigidbodies)
            {
                rigidbody.isKinematic = true;
            }
        }
        
        public void ShowDeath()
        {
            foreach(var rigidbody in GetComponentsInChildren<Rigidbody>())
            {
                rigidbody.isKinematic = false;
            }
            gameObject.GetComponentInParent<Animator>().enabled = false;
            if (!_isDead)
            {
                _globalValuesManager.QuantityEnemy++;
                _globalUIManager.QuantityEnemyText.text = _globalValuesManager.QuantityEnemy.ToString();
                gameObject.GetComponentInParent<AudioSource>().Play();
                GetComponent<NavMeshAgent>().enabled = false;
                EnemySpawner.OnEnemyDead();

            }
            _isDead = true;
        }
        
        private void Update()
        {
            if (!_isDead)
            {
                _navMeshAgent.SetDestination(_playerPosition.transform.position);
            }
        }
        
        public void AnimatorHit()
        {
            if (!_isDead)
            {
                StartCoroutine(StopEnemyWalking(3.5f));
                _animator.SetTrigger(_hit);
                _playerController._hpPlayer --;
                _globalUIManager._hpText.text = _playerController._hpPlayer.ToString();
            }
        }

        private IEnumerator StopEnemyWalking(float second)
        {
            _navMeshAgent.speed = 0f;
            yield return new WaitForSeconds(second);
            _navMeshAgent.speed = 3.5f;
        }
    }
}