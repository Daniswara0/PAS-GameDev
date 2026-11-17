using UnityEngine;

public class Enemy : MonoBehaviour 
{
    public float ms = 2f; // movement speed
    protected Transform player;
    // private int facingDirect = -1;

    [Header("Pengaturan State Machine")]
    [SerializeField] private float jarakDeteksi = 6f;
    [SerializeField] private float jarakSerang = 1.2f;
    [SerializeField] private float jedaSerang = 1f;
    
    private StateEnemy state = StateEnemy.IDLE;
    private float waktuSerangTerakhir;

    protected virtual void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        PeriksaTransisi();

        switch (state)
        {
            case StateEnemy.IDLE: PerilakuIdle(); break;
            case StateEnemy.PATROL: PerilakuPatrol(); break;
            case StateEnemy.CHASE: PerilakuChase(); break;
            case StateEnemy.ATTACK: PerilakuAttack(); break;
        }
    }

    public float JarakKePlayer()
    {
        if (player == null) return Mathf.Infinity;
        return Vector2.Distance(transform.position, player.position);
    }

    void PeriksaTransisi()
    {
        float jarak = JarakKePlayer();

        switch (state)
        {
            case StateEnemy.IDLE:
            case StateEnemy.PATROL:
                if (jarak <= jarakDeteksi)
                {
                    state = StateEnemy.CHASE;
                }
                break;

            case StateEnemy.CHASE:
                if (jarak <= jarakSerang && Time.time - waktuSerangTerakhir >= jedaSerang)
                {
                    state = StateEnemy.ATTACK;
                    waktuSerangTerakhir = Time.time;
                }
                else if (jarak > jarakDeteksi)
                {
                    state = StateEnemy.IDLE;
                }
                break;

            case StateEnemy.ATTACK:
                state = StateEnemy.CHASE;
                break;
        }
    }

    public void Kejar()
    {
        if (player == null) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            ms * Time.deltaTime
        );
    }

    public virtual void Serang()
    {
        Debug.Log("Enemy menyerang!");
    }

    // WAJIB ADA: Implementasi method dari IDamageable agar error hilang

    // private void Mati()
    // {
    //     Debug.Log($"{gameObject.name} mati!");
    //     Destroy(gameObject);
    // }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.ChangeHealth(-10);
                Debug.Log("Player terkena damage dari musuh!");
            }
        }
    }

    void PerilakuIdle() { }
    void PerilakuPatrol() { Debug.Log("Enemy sedang patrol"); }
    void PerilakuChase() { Kejar(); Debug.Log("Enemy sedang mengejar player"); }
    void PerilakuAttack() { Debug.Log("Enemy sedang menyerang player"); }
}