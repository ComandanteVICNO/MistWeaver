using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyAttackNew : MonoBehaviour
{
    [SerializeField] Coroutine attackCoroutine;

    public PlayerHealth playerHealth;
    public NormalEnemyLogicManager manager;
    public float attackCooldown;
    public float attackCooldownBonus;

    public AnimationClip attackClip;
    public Animator spriteAnimator;
    bool isAttacking = false;
    bool canAttack = true;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        attackCooldown = attackClip.length + attackCooldownBonus;
    }

    public void DoAttack()
    {
        if (canAttack)
        {
            attackCoroutine = StartCoroutine(Attack());

        }
    }

    IEnumerator Attack()
    {
        canAttack = false;
        spriteAnimator.SetBool("isAttacking", true);
        manager.playerHealth.TakeDamage(manager.damageValue);
        yield return new WaitForSecondsRealtime(attackClip.length);

        spriteAnimator.SetBool("isAttacking", false);

        yield return new WaitForSecondsRealtime(attackCooldown);
        canAttack = true;
    }

}
