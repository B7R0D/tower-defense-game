using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable, ITargetable, IStatusEffectReceiver
{
    public float maxHealth = 10;
    private float health;

    private NavMeshAgent agent;
    private Transform target;

    void Awake()
    {
        health = maxHealth;
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        // zoek het kasteel op tag
        target = GameObject.FindWithTag("PlayerBase").transform;

        // stel bestemming
        agent.SetDestination(target.position);
    }

    public Vector3 GetPosition() => transform.position;
    public bool IsAlive => health > 0;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0) Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public void ApplySlow(float percent, float duration) { }
    public void ApplyPoison(float dps, float duration) { }
}
