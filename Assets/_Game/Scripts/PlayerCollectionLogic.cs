using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerCollectionLogic : MonoBehaviour
{
    [SerializeField] private float attackRange;
    [SerializeField] private float attackTimeOut;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private ParticleSystem hitParticle;
    [SerializeField] private ParticleSystem collectedParticle;
    private List<Collectable> allCollectables = new List<Collectable>();
    [CanBeNull] private TimeCounter attackTimeCounter;
    public static Action<int, CommonVar.CollectableType> OnCollected;
    private void Start()
    {
        this.allCollectables = FindObjectsOfType<Collectable>().ToList();
        attackTimeCounter = new TimeCounter(attackTimeOut);
    }

    private void LateUpdate()
    {
        Collectable closest = FindClosestPosition();
        if (closest && attackTimeCounter.IsTickFinished(Time.deltaTime))
        {
            playerAnimator.SetTrigger("Attack");
            if (closest.OnHit())
            {
                OnCollected?.Invoke(1,closest.GetCollectableType());
                this.collectedParticle.Simulate(0,true);
                this.collectedParticle.Play();                
            }
            else
            {
                
                this.hitParticle.Simulate(0,true);
                this.hitParticle.Play();
            }
        }

    }
    protected Collectable FindClosestPosition()
    {
        float minDis = attackRange;
        Collectable closest = null;
        foreach (Collectable targetPos in allCollectables)
        {
            if (!targetPos.IsReadyToCollect)
            {
                continue;
            }
            
            float distance = Vector3.Distance(targetPos.transform.position,
                this.gameObject.transform.position);
            if (distance <= minDis)
            {
                closest = targetPos;
                minDis = distance;
            }
        }

        return closest;
    }
}
