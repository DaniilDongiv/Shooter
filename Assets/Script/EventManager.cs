using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action EnemyDead;

    public static void OnEnemyDead()
    {
        EnemyDead?.Invoke();
    }
}
