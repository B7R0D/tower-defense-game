using UnityEngine;
using UnityEngine.UI;

public class BaseHealth : MonoBehaviour
{
    public float maxHealth = 225f;
    public float currentHealth;

    public Slider healthSlider;

    private void Start()
    {
        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Touched by: " + other.name);
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                TakeDamage(enemy.damageToBase);   // Enemy doet damage aan base
            }

            Destroy(other.gameObject); // Enemy verdwijnt na hit
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (healthSlider != null)
            healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("GAME OVER");
            Time.timeScale = 0f;
        }
    }

}
