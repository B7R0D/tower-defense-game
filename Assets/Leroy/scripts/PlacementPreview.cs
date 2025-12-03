using UnityEngine;

public class PlacementPreview : MonoBehaviour
{
    public LayerMask groundMask;
    public Color validColor = Color.green;
    public Color invalidColor = Color.red;

    private Renderer[] rends;
    private bool canPlace = false;

    void Start()
    {
        rends = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        FollowMouse();
        CheckPlacement();
    }

    void FollowMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundMask))
        {
            transform.position = hit.point;
        }
    }

    void CheckPlacement()
    {
        // Checkt of er andere torens te dichtbij staan
        Collider[] hits = Physics.OverlapSphere(transform.position, 0.5f);

        if (hits.Length > 0)
        {
            SetColor(invalidColor);
            canPlace = false;
        }
        else
        {
            SetColor(validColor);
            canPlace = true;
        }

        // Als je klikt en mag plaatsen → zet echte toren
        if (Input.GetMouseButtonDown(0) && canPlace)
        {
            // wordt later hier echte tower geplaatst
            Debug.Log("PLACE TOWER!");
        }
    }

    void SetColor(Color c)
    {
        foreach (Renderer r in rends)
        {
            r.material.color = c;
        }
    }
}
