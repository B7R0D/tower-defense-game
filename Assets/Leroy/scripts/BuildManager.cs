using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    public GameObject selectedTowerPrefab;
    private bool isPlacing = false;

    private int currentTowerCost = 100; // ← KOSTEN VAN JE TOWER

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        // Start placing tower (bijv. knop 1)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TryStartPlacement();
        }

        // Tower neerzetten
        if (isPlacing && Input.GetMouseButtonDown(0))
        {
            TryPlaceTower();
        }
    }

    // PROBEERT placement mode te starten (met geld check)
    public void TryStartPlacement()
    {
        // ---> FIX 1: CHECK GELD EERST
        if (!MoneyManager.Instance.SpendMoney(currentTowerCost))
        {
            Debug.Log("Niet genoeg geld.");
            return;
        }

        // Geld is betaald → nu pas begin je met plaatsen
        isPlacing = true;
    }

    public bool IsPlacing()
    {
        return isPlacing;
    }

    public void StopPlacing()
    {
        isPlacing = false;
    }

    void TryPlaceTower()
    {
        if (selectedTowerPrefab == null)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            IBuildableSpot tile = hit.collider.GetComponent<IBuildableSpot>();

            if (tile != null && tile.IsFree)
            {
                tile.PlaceTower(selectedTowerPrefab);
                StopPlacing();
            }
        }
    }
}
