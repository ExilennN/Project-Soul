using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float weakAttackDamage = 15f;
    [SerializeField] private float strongAttackDamage = 30f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private PlayerAnimation playerAnimation;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            WeakAttack();
        }

        if (Input.GetMouseButtonDown(1))
        {
            StrongAttack();
        }
    }

    private void WeakAttack()
    {
        playerAnimation.SetSwordAttackAnimation();

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealthBar enemyHealth = enemy.GetComponent<EnemyHealthBar>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(weakAttackDamage);
                Debug.Log("Hit enemy with weak attack: " + weakAttackDamage + " damage");
            }
        }
    }

    private void StrongAttack()
    {
        playerAnimation.SetSwordStabAnimation();

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealthBar enemyHealth = enemy.GetComponent<EnemyHealthBar>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(strongAttackDamage);
                Debug.Log("Hit enemy with strong attack: " + strongAttackDamage + " damage");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
