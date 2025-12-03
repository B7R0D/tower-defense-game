using UnityEngine;

public class TowerPlacementManager : MonoBehaviour
{
    public GameObject ghostPrefab; // de hologram variant
    private GameObject currentGhost;

    // ---------------------- NEW CODE ----------------------
    public TowerData towerData;   // Tower ScriptableObject
    public LayerMask placementMask; // optioneel: waar je mag bouwen
    // ------------------------------------------------------

    void Update()
    {
        // Als BuildManager niet in placement mode is, destroy ghost en stop
        if (!BuildManager.Instance.IsPlacing())
        {
            if (currentGhost != null)
            {
                Destroy(currentGhost);
                currentGhost = null;
            }
            return;
        }

        // Als we in placement mode zitten en er is nog geen ghost, maak er 1
        if (currentGhost == null)
        {
            currentGhost = Instantiate(ghostPrefab);
            SetGameObjectIgnoreRaycast(currentGhost, true); // voorkomt dat ghost raycasts blokkeert
            DisableAllColliders(currentGhost); // extra zekerheid
            Debug.Log("Ghost instantiated.");
        }

        // Raycast naar muis en verplaats ghost
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            float height = ghostPrefab.transform.localScale.y / 2f;
            currentGhost.transform.position = hit.point + new Vector3(0, height, 0);

            // ---------------------- NEW CODE ----------------------
            // Klik om tower te plaatsen
            if (Input.GetMouseButtonDown(0))
            {
                TryPlaceTower(hit.point);
            }
            // ------------------------------------------------------
        }
    }

    // ---------------------- NEW CODE ----------------------
    void TryPlaceTower(Vector3 position)
{
    // basic null guards
    if (towerData == null)
    {
        Debug.LogError("towerData is null in TowerPlacementManager!");
        return;
    }

    if (MoneyManager.Instance == null)
    {
        Debug.LogError("MoneyManager.Instance is null! Make sure MoneyManager exists in the scene.");
        return;
    }

    // voorkom meerdere plaastingen per klik / als er geen active ghost is
    if (currentGhost == null)
    {
        Debug.Log("No ghost to place on.");
        return;
    }

    int cost = towerData.cost;
    Debug.Log($"Attempting to place tower. Cost: {cost}. Current money: {MoneyManager.Instance.money}");

    // Als er genoeg geld is, trateer de transactie atomair: eerst check & spend, dan instantiate.
    bool paid = MoneyManager.Instance.SpendMoney(cost);
    if (!paid)
    {
        // Geef feedback (debug + eventueel visuele feedback)
        Debug.Log("Niet genoeg geld om tower te plaatsen.");
        // optioneel: speel geluid of flash UI
        return;
    }

    // Als we hier zijn: betaald => instantiate tower
    Instantiate(towerData.towerPrefab, position, Quaternion.identity);

    // Ghost verwijderen & stop placement
    Destroy(currentGhost);
    currentGhost = null;
    BuildManager.Instance.StopPlacement();

    Debug.Log("Tower geplaatst!");
}

    // Zet object en children op Ignore Raycast (layer 2) of terugzetten
    void SetGameObjectIgnoreRaycast(GameObject go, bool ignore)
    {
        int layer = ignore ? 2 : 0; // 2 = Ignore Raycast (Unity default)
        foreach (Transform t in go.GetComponentsInChildren<Transform>(true))
        {
            t.gameObject.layer = layer;
        }
    }

    // Disable colliders op ghost (zodat raycasts niet op hem landen)
    void DisableAllColliders(GameObject go)
    {
        var cols = go.GetComponentsInChildren<Collider>(true);
        foreach (var c in cols)
            c.enabled = false;
    }
}
