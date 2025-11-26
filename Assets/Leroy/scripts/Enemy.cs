using UnityEngine;

public enum EnemyType
{
    Normal,
    Immune
}

public class Enemy : MonoBehaviour
{
    public EnemyType enemyType = EnemyType.Normal;
}
