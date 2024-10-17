using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private int hitCount;
    [SerializeField] private float disableTimeOut = 2F;
    [SerializeField] private CommonVar.CollectableType collectableType;
    [SerializeField]
    private int hitLeftToCollect;
    public bool IsReadyToCollect = true;
    public static Action<Transform, CommonVar.CollectableType> OnItemCollected;
    private void OnEnable()
    {
        hitLeftToCollect = hitCount;
    }

    // bool is collected.
    public bool OnHit()
    {
        Debug.Log("Object Hit");
        // play anim or ..
        hitLeftToCollect--;
        if (hitLeftToCollect <= 0F)
        {
            IsReadyToCollect = false;
            this.gameObject.transform.DOScale(Vector3.zero, .3F).SetEase(Ease.InOutBounce).OnComplete(() =>

            {
                OnItemCollected?.Invoke(this.transform,this.collectableType);
                StartCoroutine(ReEnableAfterCollected());    
            });
            
            
            // collect that
            return true;
        }

        return false;
    }

    IEnumerator ReEnableAfterCollected()
    {
        
        yield return new WaitForSeconds(this.disableTimeOut);
        this.IsReadyToCollect = true;
        this.gameObject.transform.DOScale(Vector3.one, .3F).SetEase(Ease.InOutBounce).OnComplete(
            () => { this.hitLeftToCollect = hitCount; }
        );
    }
    

    public CommonVar.CollectableType GetCollectableType() => collectableType;
}