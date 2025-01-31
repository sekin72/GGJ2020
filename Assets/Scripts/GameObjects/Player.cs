﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Animator Animator;
    public Camera camera;
    public bool Courage = false;
    public bool IdleStarted, RunningStarted, AttackStarted;

    protected override void Die()
    {
        GameManager.Instance.Die();
    }

    public void StartIdleAnim()
    {
        if (!IdleStarted)
        {
            IdleStarted = true;
            RunningStarted = false;
            AttackStarted = false;
            if (Courage)
                Animator.Play("Idle", 0, 0);
            else
                Animator.Play("Terrified", 0, 0);
        }
    }

    public void StartRunningAnim()
    {
        if (!RunningStarted)
        {
            IdleStarted = false;
            RunningStarted = true;
            AttackStarted = false;
            Animator.Play("Running", 0, 0);
        }
    }

    public void StartAttackAnim()
    {
        if (!AttackStarted)
        {
            IdleStarted = false;
            RunningStarted = false;
            AttackStarted = true;
            Animator.Play("Attack", 0, 0);
            StartCoroutine(WaitUntilAnimFinish());
        }
    }

    private IEnumerator WaitUntilAnimFinish()
    {
        yield return new WaitForSeconds(2.2f);
        StartIdleAnim();
    }
}
