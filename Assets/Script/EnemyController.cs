using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Script
{
    public class EnemyController : MonoBehaviour
    {
        public Rigidbody[] Rigidbodies;
        
        [SerializeField]
        private Transform _playerPosition;
        [SerializeField]
        private GlobalValuesManager _globalValuesManager;
        [SerializeField]
        private NavMeshAgent _navMeshAgent;
        [SerializeField] 
        private PlayerController _playerController;
        [SerializeField]
        private GlobalUIManager _globalUIManager;
        
        private bool _isDead;
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
            _globalUIManager.QuantityEnemyText.text = _globalValuesManager.QuantityEnemy.ToString();
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
                _globalValuesManager.QuantityEnemy--;
                _globalUIManager.QuantityEnemyText.text = _globalValuesManager.QuantityEnemy.ToString();
                gameObject.GetComponentInParent<AudioSource>().Play();
                GetComponent<NavMeshAgent>().enabled = false;
            }
            _isDead = true;
            
        }
        
        void Update()
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
                _playerController._hp --;
                _globalUIManager._hpText.text = _playerController._hp.ToString();
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