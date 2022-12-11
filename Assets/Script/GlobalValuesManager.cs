using Script;
using UnityEngine;

public class GlobalValuesManager : MonoBehaviour
{   
    [HideInInspector]
    public int QuantityEnemy;
    
    [SerializeField]
    private EnemySpawner _enemySpawner;

    private void Start()
    {
        QuantityEnemy = _enemySpawner.QuantityEnemy();
    }
}
