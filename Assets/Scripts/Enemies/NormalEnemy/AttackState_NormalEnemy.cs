 using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class AttackState_NormalEnemy : LogicMachineBehaviour<NormalEnemyLogicManager>
{
    bool canAttack;
    public bool isAttacking;
    public bool isWaitingCooldown;



    float attackCooldown;
    public float currentAttackCooldown;


    

    float animationCooldown;
    float currentAnimationCooldown;

   
    public override void OnAwake()
    {

    }

    public override void OnEnter()
    {

        attackCooldown = manager.attackAnimation.length + manager.cooldownBonus;
        animationCooldown = manager.attackAnimation.length;
        currentAttackCooldown = attackCooldown;
        currentAnimationCooldown = animationCooldown;

        canAttack = true;
        isAttacking = false;
        isWaitingCooldown = false;

        logicAnimator.SetBool("canChasePlayer", false);
    }
    public override void OnUpdate()
    {
        if (!isAttacking)
        {
            if (!manager.attackHitbox.isPlayerDetected)
            {
                logicAnimator.SetBool("canAttackPlayer", false);
            }


        }
        if (manager.enemyHealth.wasAttacked)
        {
            logicAnimator.SetBool("wasAttacked", true);
        }

        StopMovement();

        manager.enemyAttack.DoAttack();

    }

    public override void OnExit()
    {

        logicAnimator.SetBool("canAttackPlayer", false);
        manager.animator.SetBool("isAttacking", false);
    }




    //void NewAttack()
    //{
    //    // If the attack is not on cooldown and the player is detected
    //    if (canAttack && manager.attackHitbox.isPlayerDetected)
    //    {
    //        // Set attacking state
    //        isAttacking = true;
    //        canAttack = false;

    //        // Trigger attack animation
    //        manager.animator.SetBool("isAttacking", true);

    //        // Deal damage to the player
    //        manager.playerHealth.TakeDamage(manager.damageValue);
    //    }

    //    // If the attack is on cooldown
    //    if (!canAttack)
    //    {
    //        // Update attack cooldown
    //        currentAttackCooldown -= Time.deltaTime;

    //        // If the attack cooldown is finished
    //        if (currentAttackCooldown <= 0)
    //        {
    //            // Reset attacking state and allow the next attack
    //            isAttacking = false;
    //            canAttack = true;
    //            currentAttackCooldown = attackCooldown;
    //        }
    //    }

    //    // Update animation cooldown
    //    currentAnimationCooldown -= Time.deltaTime;

    //    // If the animation cooldown is finished
    //    if (currentAnimationCooldown <= 0)
    //    {
    //        manager.animator.SetBool("isAttacking", false);

    //        // Reset animation cooldown
    //        currentAnimationCooldown = animationCooldown;
    //    }
    //}

    void StopMovement()
    {
        manager.rb.velocity = Vector3.zero;
    }
}