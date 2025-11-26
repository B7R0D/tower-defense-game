using System.Collections.Generic;
using UnityEngine;

public class BasicTower : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> targetlist = new List<GameObject>();


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Enemy"))
            targetlist.Add(collider.gameObject);
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Enemy"))
            targetlist.Remove(collider.gameObject);
    }

    void Update()
    {
        Debug.DrawRay(transform.position, targetlist[0].transform.position - transform.position, Color.red, 0.1f); 
    }

}