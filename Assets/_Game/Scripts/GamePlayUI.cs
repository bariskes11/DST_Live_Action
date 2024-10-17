using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtTree;
    [SerializeField] private TextMeshProUGUI txtRock;
    private int totalCollectedRock;
    private int totalCollectedTree;

    private void Start()
    {
        PlayerCollectionLogic.OnCollected += OnPlayerCollected;
        txtTree.text = "0";
        txtRock.text = "0";
    }

    private void OnDestroy()
    {
        PlayerCollectionLogic.OnCollected -= OnPlayerCollected;
    }

    void OnPlayerCollected(int val, CommonVar.CollectableType type)
    {
        if (type == CommonVar.CollectableType.Rock)
        {
            totalCollectedRock++;
        }
        else if (type == CommonVar.CollectableType.Tree)
        {
            totalCollectedTree++;
        }

        txtTree.text = totalCollectedTree.ToString();
        txtRock.text = totalCollectedRock.ToString();
    }
}