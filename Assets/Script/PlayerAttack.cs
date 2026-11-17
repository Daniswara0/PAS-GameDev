using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float weaponRange = 1;
    public LayerMask enemyLayer;
    public int damage = 15;
    public Animator anim;

    public void Attack()
    {
        Debug.Log("Player menyerang!");  

        if (anim != null)
        {
            anim.SetBool("isAttacking", true);
            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, enemyLayer);

            if (enemies.Length > 0)
            {
                enemies[0].GetComponent<EnemyHealth>().KenaDamage(damage);
                Debug.Log("Musuh terkena hit!");
            }
        }
        else
        {
            Debug.LogError("Variabel Anim di PlayerAttack belum di-assign!");
        }
    }

    public void FinishAttacking()
    {
        anim.SetBool("isAttacking", false);
    }
}
