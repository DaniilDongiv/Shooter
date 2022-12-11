using System.Collections.Generic;
using UnityEngine;

public class SelectedPlayerHandler : MonoBehaviour
{
    [SerializeField]
    private Player[] _playersPrefab;

    private Dictionary<string, Player> _playerPrefabsByName = new();

    public void Awake()
    {
        foreach (var playerPrefab in _playersPrefab)
        {
            _playerPrefabsByName.Add(playerPrefab.name,playerPrefab);
        }
        DontDestroyOnLoad(this);
    }

    public Player GetLastSelectedPlayer()
    {
        var playerPrefabName = PrefsManager.Load();

        return _playerPrefabsByName.ContainsKey(playerPrefabName)
            ? _playerPrefabsByName[playerPrefabName] : _playersPrefab[0];
    }

    public void SetLastSelectedPLayer(Player player)
    {
        PrefsManager.Save(player.Prefab.name);
    }
}
