using UnityEngine;

public class BuildTile : MonoBehaviour, IBuildableSpot
{
    public Transform buildPoint;        // Waar de toren precies moet staan
    public GameObject currentTower;     // Welke toren staat er op deze tile?

    public bool IsFree => currentTower == null;

    private void Start()
    {
        // Als er geen buildPoint is, gebruik dan het object zelf
        if (buildPoint == null)
            buildPoint = this.transform;
    }

    public Vector3 GetBuildPosition()
    {
        return buildPoint.position;
    }

    public void PlaceTower(GameObject towerPrefab)
    {
        if (!IsFree) return;

        // ----------- HOOGTE FIX --------------
        // hoogte berekenen op basis van de schaal van de toren
        float heightOffset = towerPrefab.transform.localScale.y / 2f;

        Vector3 correctPos = GetBuildPosition();
        correctPos.y += heightOffset;
        // --------------------------------------

        currentTower = Instantiate(towerPrefab, correctPos, Quaternion.identity);
    }
}
