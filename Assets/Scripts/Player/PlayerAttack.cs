using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack current;

    [Header("References")]
    public Animator animator;
    public PlayerMovController movController;
    public Transform attackPoint;
    public AudioSource attackSound;
    public Rigidbody rb;

    [Header("Player Damage & Range")]
    public float attackRange = 0.5f;
    public float playerDamage;
    public int maxEnergy;
    public int currentEnergy;
    public float playerStunningDamage;
    public float playerDamageFinal;
    public float knockBackAmount;

    [Header("Attack and Animation Cooldown")]
    public float attackCooldown;
    public float attackCooldownBonus;

    [Header("Attack Checks")]
    public bool isAttacking = false;
    public bool isAttackAllow = true;
    public LayerMask enemyLayers;
    public bool canAttack;

    [Header("Animation Clip References")]
    public AnimationClip normalAttackAnimation;
    public AnimationClip stunAttackAnimation;

    [Header("Animation Clip Durations")]
    public float normalAttackAnimationTime;
    public float stunAttackAnimationTime;

    [Header("Energia UI")]
    public Image stunCharge1;
    public Image stunCharge2;
    public Image stunCharge3;

    public Color32 originalColor;
    public Color32 depletedColor;

    public Coroutine normalAttackCoroutine;
    public Coroutine stunAttackCoroutine;

    private int attack = 0;

    private void Awake()
    {
        
    }
    private void Start()
    {
        canAttack = true;
        current = this;
        currentEnergy = maxEnergy;
        rb = GetComponent<Rigidbody>();
        normalAttackAnimationTime = normalAttackAnimation.length;
        stunAttackAnimationTime = stunAttackAnimation.length;

        originalColor = stunCharge1.color;
        depletedColor = new Color32(50, 50, 50, 255);
        
    }


    void Update()
    {

        //Verify attack input
        if (UserInput.instance.controls.Player.MainAttack.WasPressedThisFrame() && movController.isGrounded && canAttack)
        {
            if (!isAttackAllow) return;
            if (normalAttackCoroutine == null && !isAttacking)
            {
                normalAttackCoroutine = StartCoroutine(DoMainAttack());
            }
            
        }

        if(UserInput.instance.controls.Player.StunAttack.WasPerformedThisFrame() && movController.isGrounded && canAttack)
        {
            if (!isAttackAllow) return;
            
            if (currentEnergy == 0) return;
            else
            {
                if (stunAttackCoroutine == null && !isAttacking)
                {
                    stunAttackCoroutine = StartCoroutine(DoStunAttack());
                    currentEnergy -= 1;
                    
                }
            }
        }

        UpdateEnergyUI();
    }


    public IEnumerator DoMainAttack()
    {
        float attackCooldown = normalAttackAnimationTime + attackCooldownBonus;
        rb.velocity = Vector3.zero;
        animator.SetBool("NormalAttack", true);
        isAttacking = true;
        movController.canJump = false;
        movController.canDash = false;
        movController.canMove = false;


        MainAttack();
     

        yield return new WaitForSeconds(normalAttackAnimationTime);

        animator.SetBool("NormalAttack", false);

        yield return new WaitForSeconds(attackCooldownBonus);
        
        isAttacking = false;

        movController.canJump = true;
        movController.canDash = true;
        movController.canMove = true;

        normalAttackCoroutine = null;
    }

    public IEnumerator DoStunAttack()
    {
        float attackCooldown = stunAttackAnimationTime + attackCooldownBonus;
        rb.velocity = Vector3.zero;
        animator.SetBool("StunAttack", true);
        isAttacking = true;

        movController.canJump = false;
        movController.canDash = false;
        movController.canMove = false;
        

        StunnedAttack();

        yield return new WaitForSeconds(stunAttackAnimationTime);

        animator.SetBool("StunAttack", false);

        yield return new WaitForSeconds(attackCooldownBonus);
        isAttacking = false;

        movController.canJump = true;
        movController.canDash = true;
        movController.canMove = true;

        stunAttackCoroutine = null;
    }


    void MainAttack()
    {
        

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.GetComponent<SmallEnemyHealth>() != null)
            {
                enemy.GetComponent<SmallEnemyHealth>().TakeDamage(playerDamage);
            }

            if (enemy.GetComponent<NormalEnemyHealth>() != null)
            {
                enemy.GetComponent<NormalEnemyHealth>().TakeDamage(playerDamage);
            }

            if (enemy.GetComponent<Fracture>() != null)
            {
                enemy.GetComponent<Fracture>().BreakObject();
                Debug.Log("break");
            }

        }

    }

    public void StunnedAttack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        
        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.GetComponent<SmallEnemyHealth>() != null)
            {
                
                enemy.GetComponent<SmallEnemyHealth>().TakeStunnedDamage(playerStunningDamage);
            }
            if (enemy.GetComponent<NormalEnemyHealth>() != null)
            {
                
                enemy.GetComponent<NormalEnemyHealth>().TakeStunnedDamage(playerStunningDamage);
            }

            if (enemy.GetComponent<Fracture>() != null)
            {
                enemy.GetComponent<Fracture>().BreakObject();
                
            }

        }
    }

    public void UpdateEnergyUI()
    {
        switch (currentEnergy)
        {
            case 0:
                stunCharge1.color = depletedColor;
                stunCharge2.color = depletedColor;
                stunCharge3.color = depletedColor;
                break;

            case 1:
                stunCharge1.color = originalColor;
                stunCharge2.color = depletedColor;
                stunCharge3.color = depletedColor;
                break;

            case 2:
                stunCharge1.color = originalColor;
                stunCharge2.color = originalColor;
                stunCharge3.color = depletedColor;
                break;
            
            case 3:
                stunCharge1.color = originalColor;
                stunCharge2.color = originalColor;
                stunCharge3.color = originalColor;
                break;
        }
            
    }

    public void EnergyPickup()
    {
        currentEnergy += 1;

        if(currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }

    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

    }
}
