using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GameManager : NetworkBehaviour
{
    public static GameManager Singleton;
    public Transform CanvasTransform;
    public GameObject ServerQuestionsPrefab;
    private void Awake()
    {
        Singleton = this;
    }
    public void StartGame()
    {
        //if (!IsServer) return;
        //Instantiate(ServerQuestionsPrefab, CanvasTransform);
    }
}
