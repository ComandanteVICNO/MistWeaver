using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.VFX;

public class ChargeState_SmallEnemy : LogicMachineBehaviour<SmallEnemyLogicManager>
{
    [Header("VFX")]
    
    
    VisualEffect vfx;

    float originalCharge;
    float currentCharge;
    bool canCharge;
    private CancellationTokenSource cancellationTokenSource;
    
    public override void OnAwake()
    {
        cancellationTokenSource = new CancellationTokenSource();
    }

    public override void OnEnter()
    {
        SpawnVFX();
        logicAnimator.SetBool("canLunge", false);
        manager.spriteAnimator.SetBool("isCharging", true);
        canCharge = true;
        originalCharge = manager.lungeChargeTime;
        currentCharge = originalCharge;
        cancellationTokenSource = new CancellationTokenSource();

        if (manager.attackPlayerTransform != null)
        {
            manager.initialPlayerPosition = manager.attackPlayerTransform.position;
        }
    }
    public override void OnUpdate()
    {
        if (manager.enemyHealth.wasAttacked)
        {
            logicAnimator.SetBool("wasAttacked", true);
        }

        ChargeLunge();

        StopMovement();
    }

    public override void OnExit()
    {
        logicAnimator.SetBool("canCharge", false);
        manager.spriteAnimator.SetBool("isCharging", false);
        cancellationTokenSource?.Cancel();
        cancellationTokenSource?.Dispose();
        canCharge = false;
    }

    void StopMovement()
    {
        manager.rb.velocity = Vector3.zero;
    }

    async void ChargeLunge()
    {
        bool wasParticlesAtivated = false;
        var time = manager.lungeChargeTime;
        if (!wasParticlesAtivated)
        {
            
            wasParticlesAtivated = true;
        }

        try
        {
            await Task.Delay(TimeSpan.FromSeconds(time), cancellationTokenSource.Token);
        }
        catch (TaskCanceledException)
        {
            
            return;
        }
        if (!active) return;

        logicAnimator.SetBool("canLunge", true);
        wasParticlesAtivated = false;


    }
    void SpawnVFX()
    {
        vfx = Instantiate(manager.warnAttack);
        vfx.transform.position = transform.position;
        vfx.Play();
        Destroy(vfx.gameObject, manager.vfxTimeUntilDie);
    }

}
