using UnityEngine;

public class Player : MonoBehaviour
{
    public Player Prefab => _prefab;

    [SerializeField] private Player _prefab;
}
