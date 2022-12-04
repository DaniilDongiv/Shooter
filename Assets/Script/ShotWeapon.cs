using System.Collections;
using Script;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ShotWeapon : MonoBehaviour
{
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
    private ParticleSystem _fxBarrels;
    [SerializeField]
    private EnemyController _enemyController;
    [SerializeField]
    private AnimationAim _aimAnim;
    [SerializeField]
    private GameObject _weapon;
    [SerializeField]
    private GlobalUIManager _globalUIManager;
    [SerializeField] 
    private Camera _camera;
    
    public int Bullets = 20;
    private float _timer;
    private bool _isAiming = true;

    private void Start()
    {
        _globalUIManager.QuantityBullet.text = Bullets.ToString();
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (Input.GetMouseButton(0))
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
                        barrels.GetComponentInParent<AudioSource>().Play();
                        var fxbarrels = Instantiate(_fxBarrels, barrels.transform);
                        fxbarrels.transform.SetParent(null);
                        barrels.GetComponent<Rigidbody>().AddForce(0f,5000f,0f);
                        
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

        if (Input.GetMouseButtonDown(1))
        {
            if (_isAiming)
            {
                _weapon.GetComponent<Animator>().SetTrigger("Aiming");
                StartCoroutine(Aiming());
                _isAiming = !_isAiming;
            }
            else if(!_isAiming)
            {
                _weapon.GetComponent<Animator>().SetTrigger("DontAiming");
                StartCoroutine(Aiming());
                _isAiming = true;
            }
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