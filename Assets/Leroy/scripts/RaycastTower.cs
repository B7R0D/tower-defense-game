using UnityEngine;

public class RaycastTower : MonoBehaviour
{
    public TowerData data; // ← ScriptableObject met de stats

    private float fireTimer;

    void Update()
    {
        if (data == null) return;

        GameObject target = FindClosestEnemy();

        if (target == null) return;

        // Richt alleen horizontaal
        Vector3 dir = (target.transform.position - transform.position);
        dir.y = 0;

        // Rotatie
        if (dir != Vector3.zero)
        {
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * data.rotationSpeed);
        }

        // Debug lijn
        Debug.DrawRay(transform.position, dir.normalized * data.range, Color.red, 0.05f);

        // Schieten
        fireTimer += Time.deltaTime;
        if (fireTimer >= data.fireRate)
        {
            Shoot(target);
            fireTimer = 0f;
        }
    }

    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject closest = null;
        float closestDist = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null) continue;

            float dist = Vector3.Distance(transform.position, enemy.transform.position);

            if (dist < closestDist && dist <= data.range)
            {
                closest = enemy;
                closestDist = dist;
            }
        }
        return closest;
    }

    void Shoot(GameObject target)
    {
        Vector3 dir = (target.transform.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, dir, out RaycastHit hit, data.range))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                EnemyHealth hp = hit.collider.GetComponent<EnemyHealth>();
                if (hp != null)
                    hp.TakeDamage(data.damage);
            }
        }
    }
}
