using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro;

public class Lobby : NetworkBehaviour
{
    [SerializeField] private GameObject _serverCore, _clientCore, _clientWaitingScreen, _currentTeamsServer, _waitingTextClient;
    
    // Client vars
    private string _teamName;

    // Server and Client connexion
    public override void OnNetworkSpawn()
    {
        GameManager.Singleton.CurrentState = IsServer ? GameState.ServerTurn : GameState.ClientLocked;

        if (IsClient)
        {
            AddNewTeamServerRpc(NetworkManager.Singleton.LocalClientId, _teamName);
        }

        _clientCore.SetActive(false);
        _serverCore.SetActive(IsServer ? true : false);
        _clientWaitingScreen.SetActive(IsClient ? true : false);
    }

    #region Client Lobby

    // Input field method
    public void ChangeTeamName(string newName)
    {
        _teamName = newName;
    }

    // Button method
    public void JoinGame()
    {
        if (_teamName != "")
            NetworkManager.Singleton.StartClient();
    }

    [ServerRpc(RequireOwnership = false)]
    private void AddNewTeamServerRpc(ulong id, string teamName)
    {
        GameManager.Singleton.Teams.Add(new Team(id, teamName));
        _currentTeamsServer.transform.GetChild((int)id - 1).GetComponentInChildren<TMP_Text>().text = teamName;
        UpdateTeamListClientRpc(id, teamName);
    }

    [ClientRpc]
    private void UpdateTeamListClientRpc(ulong id, string name)
    {
        if (NetworkManager.Singleton.LocalClientId != id) return;
        GameManager.Singleton.TeamInfo = new Team(id, name);
        string yourColor = "";
        switch (id)
        {
            case 1:
                yourColor = "<color=blue>Bleu</color>";
                break;
            case 2:
                yourColor = "<color=red>Rouge</color>";
                break;
            case 3:
                yourColor = "<color=yellow>Jaune</color>";
                break;
            case 4:
                yourColor = "<color=green>Vert</color>";
                break;
        }

        _waitingTextClient.GetComponent<TMP_Text>().text = $"Bienvenue <b>{name}</b> ! Votre couleur est le {yourColor}.";
    }
    #endregion
}
