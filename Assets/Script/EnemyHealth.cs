using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public int currentHealth = 60;
    public int maxHealth = 60;

    [Header("Loot Drop System")]
    public List<LootItem> lootTable;
    // [SerializeField] private GameObject healthPotionPrefab;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void KenaDamage(int jumlah)
    {
        currentHealth -= jumlah;
        Debug.Log("Musuh kena damage! Sisa darah: " + currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Musuh tewas!");
        DropLoot();
        // if (healthPotionPrefab != null)
        // {
        //     Instantiate(healthPotionPrefab, transform.position, Quaternion.identity);
        //     Debug.Log("Potion berhasil di-spawn!");
        // }
        // else
        // {
        //     Debug.LogWarning("Health Potion Prefab masih kosong di Inspector!");
        // }
        Destroy(gameObject); // Menghapus musuh dari scene
    }

    void DropLoot()
    {
        if (lootTable == null || lootTable.Count == 0) return;

        // Perulangan mengecek setiap item di dalam list Inspector
        foreach (var loot in lootTable)
        {
            float rand = Random.Range(0f, 100f);

            // Jika angka acak masuk dalam persentase dropChance
            if (rand <= loot.dropChance)
            {
                // Menentukan jumlah item yang akan keluar (antara min dan max)
                int dropAmount = Random.Range(loot.minDropAmount, loot.maxDropAmount + 1);

                for (int i = 0; i < dropAmount; i++)
                {
                    // Memberi sedikit jarak acak (offset) agar item tidak menumpuk di titik yang sama persis
                    Vector2 randomOffset = Random.insideUnitCircle * 0.5f;
                    Vector3 spawnPosition = transform.position + (Vector3)randomOffset;

                    Instantiate(loot.itemPrefab, spawnPosition, Quaternion.identity);
                }

                Debug.Log($"Berhasil drop: {loot.itemPrefab.name} sebanyak {dropAmount}");
            }
        }
    }
}