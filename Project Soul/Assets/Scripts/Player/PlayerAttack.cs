using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int weakAttackDamage = 15;
    [SerializeField] private int strongAttackDamage = 30;
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
            enemy.transform.parent.SendMessage("Damage", new AttackDetails() { damageAmout = weakAttackDamage, position = transform.position });
        }
    }

    private void StrongAttack()
    {
        playerAnimation.SetSwordStabAnimation();

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.transform.parent.SendMessage("Damage", new AttackDetails() { damageAmout = strongAttackDamage, position = transform.position });
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
