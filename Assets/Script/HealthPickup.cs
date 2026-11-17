using System;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healAmount = 20;
    [SerializeField] private float pickupRange = 2.0f;

    // DEKLARASI EVENT-NYA DI SINI
    public static event Action<int> OnHealthCollect;

    private Transform playerTransform;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        float distance = Vector2.Distance(transform.position, playerTransform.position);

        if (distance <= pickupRange)
        {
            PlayerHealth playerHealth = playerTransform.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount);
                
                // MENGIRIM / MENJALANKAN EVENT
                OnHealthCollect?.Invoke(healAmount);
                Debug.Log("Event OnHealthCollect dipanggil! Potion diambil.");

                Destroy(gameObject);
            }
        }
    }
}