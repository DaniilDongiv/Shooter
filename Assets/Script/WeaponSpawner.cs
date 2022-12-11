using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _weapon;
    
    private Player _player1;

    private void Start()
    {
        var selectedPlayerHandler = FindObjectOfType<SelectedPlayerHandler>();
        var selectedPlayer = selectedPlayerHandler.GetLastSelectedPlayer();
        
        for (int i = 0; i < _weapon.Length; i++)
        {
            if (_weapon[i].name == selectedPlayer.name)
            {
                _weapon[i].SetActive(true);
                break;
            }
        }
    }

    public GameObject[] Weapon()
    {
        return _weapon;
    }
}
