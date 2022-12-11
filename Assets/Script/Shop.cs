using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _weapon;
    [SerializeField]
    private Player[] _weaponPrefs;
    [SerializeField] 
    private SelectedPlayerHandler _selectedPlayerHandler;
    
    private int _countweapon = 0;

    private void Start()
    {
        _weapon[_countweapon].SetActive(true);
    }

    public void NextWeapon()
    {
        if (_countweapon+1 < _weapon.Length)
        {
            _weapon[_countweapon].SetActive(false);
            _countweapon++;
            _weapon[_countweapon].SetActive(true);
        }
        
        else if (_countweapon+1 >= _weapon.Length)
        {
            _weapon[_countweapon].SetActive(false);
            _countweapon = 0;
            _weapon[_countweapon].SetActive(true);
        }
    }
    public void BackWeapon()
    {
        if (_countweapon-1 >= 0)
        {
            _weapon[_countweapon].SetActive(false);
            _countweapon--;
            _weapon[_countweapon].SetActive(true);
        }
        
        else if (_countweapon-1 <= 0)
        {
            _weapon[_countweapon].SetActive(false);
            _countweapon = _weapon.Length-1;
            _weapon[_countweapon].SetActive(true);
        }
    }

    public void Choose()
    {
        _selectedPlayerHandler.SetLastSelectedPLayer(_weaponPrefs[_countweapon]);
    }
}
