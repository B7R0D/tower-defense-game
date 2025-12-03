using UnityEngine;

public interface IBuildableSpot
{
    bool IsFree { get; }
    Vector3 GetBuildPosition();
    void PlaceTower(GameObject towerPrefab);
}
