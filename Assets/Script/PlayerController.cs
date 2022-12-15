using Script;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public int _hpPlayer = 100;
    
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Camera _player;
    [SerializeField]
    private GameObject _playerGameObject;
    [SerializeField]
    private float _sensivity = 5f;
    [SerializeField]
    private float _smoothTime = 0.1f;
    [SerializeField]
    private GlobalUIManager _globalUIManager;
    [SerializeField]
    private GameObject _weapon;
    
    private float _xRot;
    private float _yRot;
    private float _xRotCurrent;
    private float _yRotCurrent;
    private float _currentVelosityX;
    private float _currentVelosityY;
    private bool _run = true;
    
    private void Update()
    {
        MouseMove();
        Move();
        
        if (Input.GetKey(KeyCode.W) && _run)
        {
            _weapon.GetComponent<Animator>().SetTrigger("run");
            _run = !_run;
        }
        if (!Input.GetKey(KeyCode.W) && !_run)
        {
            _weapon.GetComponent<Animator>().SetTrigger("stoprun");
            _run = true;
        }

        if (_hpPlayer<=0)
        {
            _globalUIManager._hpText.text = 0.ToString();
        }
    }
    private void MouseMove()
    {
        _xRot += Input.GetAxis("Mouse X") * _sensivity ;
        _yRot += Input.GetAxis("Mouse Y") * _sensivity ;
        _yRot = Mathf.Clamp(_yRot, -90, 90); 

        _xRotCurrent = Mathf.SmoothDamp(_xRotCurrent, _xRot, ref _currentVelosityX, _smoothTime);
        _yRotCurrent = Mathf.SmoothDamp(_yRotCurrent, _yRot, ref _currentVelosityY, _smoothTime);
        _player.transform.rotation = Quaternion.Euler(-_yRotCurrent, _xRotCurrent, 0f);
        _playerGameObject.transform.rotation = Quaternion.Euler(-_yRotCurrent, _xRotCurrent, 0f);
    }
    
    private void Move()
    {
        var verticalInput = Input.GetAxis("Vertical");
        var horizontalInput = Input.GetAxis("Horizontal");
        var step = Time.deltaTime * _speed;
        transform.Translate(Vector3.forward * step * verticalInput);
        var rotationAngle = Time.deltaTime * _speed;
        transform.Rotate(Vector3.up, rotationAngle * horizontalInput);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            var enemy = other.GetComponentInParent<EnemyController>();
            
            if (enemy !=null)
                enemy.AnimatorHit();
        }
    }
}
