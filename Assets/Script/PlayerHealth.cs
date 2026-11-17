using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int currentHealth = 100;
    public int maxHealth = 100;
    public Slider healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        HealthPickup.OnHealthCollect += Heal;
    }


    public void KenaDamage(int jumlah)
    {
        ChangeHealth(-jumlah);
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        healthBar.value = currentHealth;
        Debug.Log("Darah player tersisa: " + currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            gameObject.SetActive(false);
            Debug.Log("Player mati!");
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Batasi agar tidak melebihi darah maksimal
        }

        // UPDATE UI HEALTH BAR
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        Debug.Log("Darah bertambah! Sisa darah: " + currentHealth);
    }
}