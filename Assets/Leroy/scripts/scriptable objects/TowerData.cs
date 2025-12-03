using UnityEngine;

[CreateAssetMenu(fileName = "NewTowerData", menuName = "Towers/Raycast Tower Data")]
public class TowerData : ScriptableObject
{
    [Header("Tower Prefab & Cost")]
    public GameObject towerPrefab;   // ← Tower prefab
    public int cost = 100;           // ← Prijs van tower

    [Header("Targeting Settings")]
    public float range = 20f;
    public float rotationSpeed = 5f;
    public float fireRate = 1f;

    [Header("Damage Settings")]
    public float damage = 10f;
}
