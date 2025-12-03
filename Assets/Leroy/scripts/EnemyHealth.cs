using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float hp = 50f;
    public float damageToBase = 45f;  // Enemy doet 1/5 van 225 damage = 45

    public EnemySpawner spawner;

    public void TakeDamage(float dmg)
    {
        hp -= dmg;

        if (hp <= 0)
        {
            // Geef geld
            MoneyManager.Instance.AddMoney(50);

            // Wave systeem update
            if (spawner != null)
                spawner.EnemyDied();

            Destroy(gameObject);
        }
    }

}
