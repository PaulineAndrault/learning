using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro;

public class QuestionsManager : NetworkBehaviour
{
    public void CreateNewQuestion()
    {
        if (!NetworkManager.Singleton.IsServer) return;
        Debug.Log("New Quest");
        foreach(ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            CreateNewQuestionClientRpc(clientId);
        }
    }

    [ClientRpc]
    private void CreateNewQuestionClientRpc(ulong clientId)
    {
        Debug.Log("client rpc called");
        if (NetworkManager.Singleton.LocalClientId != clientId) return;
        Debug.Log("same client");
        GameObject.Find("ClientAnswerButton").GetComponentInChildren<TMP_Text>().text = $"{clientId}";
    }
}
