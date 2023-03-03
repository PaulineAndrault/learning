using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerLobby : NetworkBehaviour
{
    // Server connexion
    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
            if (GUILayout.Button("Server"))
                NetworkManager.Singleton.StartServer();
        GUILayout.EndArea();
    }

}
