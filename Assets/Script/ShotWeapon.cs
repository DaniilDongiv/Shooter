using System;
using System.Collections;
using Script;
using UnityEngine;

public class ShotWeapon : MonoBehaviour
{
    public int Bullets = 20;
    
    [SerializeField]
    private ParticleSystem _fxFire;
    [SerializeField]
    private ParticleSystem _fxDeadEnemy;
    [SerializeField]
    private Transform _positionFire;
    [SerializeField]
    private LayerMask _layerEnemy;
    [SerializeField]
    private LayerMask _layerBarrels;
    [SerializeField]
    private LayerMask _cartridges;
    [SerializeField]
    private ParticleSystem _fxBarrels;
    [SerializeField]
    private AnimationAim _aimAnim;
    [SerializeField]
    private GameObject _weapon;
    [SerializeField]
    private GlobalUIManager _globalUIManager;
    [SerializeField] 
    private Camera _camera;
    [SerializeField]
    private WeaponSpawner _weaponSpawner;
    [SerializeField]
    private GameObject _aim;
    
    private EnemyController _enemyController;
    private float _timer;
    private bool _isAiming = true;
    private event Action _selectionCartridges; 
    private event Action _shoot;
    private event Action _takeAim;
    
    private void Start()
    {
        _globalUIManager.QuantityBullet.text = Bullets.ToString();
        _selectionCartridges += OnSelectionCartridges;
        _shoot += OnShoot;
        _takeAim += OnTakeAim;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (Input.GetKey(KeyCode.F))
            _selectionCartridges?.Invoke();

        if (Input.GetMouseButton(0))
            _shoot?.Invoke();

        if (Input.GetMouseButtonDown(1))
            _takeAim?.Invoke();
    }

    private void OnSelectionCartridges()
    {
        var ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position,transform.forward * 50, Color.red);
        
        if (Physics.Raycast(ray, out RaycastHit _raycastHit, 50f, _cartridges))
        {
            var cartridges = _raycastHit.collider.gameObject;
            Bullets += 10;
            _globalUIManager.QuantityBullet.text = Bullets.ToString();
            Destroy(cartridges.gameObject);
        }
    }
    
    private void OnShoot()
    {
        if(_timer<=0)
        {
            if (Bullets>0)
            {
                var ray = new Ray(transform.position, transform.forward);
                Debug.DrawRay(transform.position,transform.forward * 50, Color.red);
                _weapon.GetComponent<Animator>().SetTrigger("shot");
                
                if (Physics.Raycast(ray, out RaycastHit _raycastHit,50f,_layerEnemy))
                {
                    var limbEnemy = _raycastHit.collider.gameObject;
                    _enemyController = limbEnemy.GetComponentInParent<EnemyController>();
                    if (_enemyController != null)
                    {
                        _enemyController.ShowDeath();
                        StartCoroutine(FxDead(limbEnemy));
                    }
                }
                
                if (Physics.Raycast(ray, out RaycastHit hitInfo,50f,_layerBarrels))
                {
                    var barrels = hitInfo.collider.gameObject;
                    var barrelsPos = transform.position;
                    barrelsPos.y = 0f;
                    barrels.GetComponentInParent<AudioSource>().Play();
                    var fxbarrels = Instantiate(_fxBarrels, barrels.transform);
                    fxbarrels.transform.SetParent(null);
                }
                
                gameObject.GetComponent<AudioSource>().Play();
                _aimAnim.AnimAim();
                Bullets--;
                _globalUIManager.QuantityBullet.text = Bullets.ToString();

                StartCoroutine(FxFire());
            }
            _timer=0.1f;
        }
    }
    
    private void OnTakeAim()
    {
        if (_isAiming)
        {
            if (_weaponSpawner.Weapon()[2].activeInHierarchy)
                _aim.SetActive(false);
            
            _weapon.GetComponent<Animator>().SetTrigger("Aiming");
            StartCoroutine(Aiming());
            _isAiming = !_isAiming;
        }
        else if(!_isAiming)
        {
            if (_weaponSpawner.Weapon()[2].activeInHierarchy)
                _aim.SetActive(true);
            
            _weapon.GetComponent<Animator>().SetTrigger("DontAiming");
            StartCoroutine(Aiming());
            _isAiming = true;
        }
    }
    
    private IEnumerator Aiming()
    {
        if (_isAiming)
        {
            for (int i = 84; i >= 74; i--)
            {
                _camera.fieldOfView = i;
                yield return new WaitForSeconds(0.02f);
            }
        }
        else if(!_isAiming)
        {
            for (int i = 74; i <= 84; i++)
            {
                _camera.fieldOfView = i;
                yield return new WaitForSeconds(0.02f);
            }
        }
    }
    
    private IEnumerator FxFire()
    {
        var fxFire = Instantiate(_fxFire, _positionFire, false);
        yield return new WaitForSeconds(0.11f); 
        Destroy(fxFire.gameObject);
    }
    
    private IEnumerator FxDead(GameObject enemy)
    {
        var fxFire = Instantiate(_fxDeadEnemy, enemy.transform);
        yield return new WaitForSeconds(1f); 
        Destroy(fxFire.gameObject);
    }
}