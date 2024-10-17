using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionCanvasManager : MonoBehaviour
{
  [SerializeField] private CollectionCanvas canvasToCreate;


  private void Start()
  {
    Collectable.OnItemCollected += CreateCollectionUI;
  }

  private void OnDestroy()
  {
    Collectable.OnItemCollected -= CreateCollectionUI;
  }

  private void CreateCollectionUI(Transform collectedItem, CommonVar.CollectableType collectionType)
  {
    Debug.Log("Called item collected!!!");
    Vector3 objectPosition = collectedItem.transform.position;
    Vector3  targetPosition  = objectPosition + new Vector3(0, 1, 0);
    CollectionCanvas canvas=  Instantiate(canvasToCreate, null);
    canvas.transform.position= targetPosition;
canvas.StartCreate(collectionType);
  }
}
