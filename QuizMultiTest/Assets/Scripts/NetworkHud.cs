using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkHud : NetworkBehaviour
{
    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            StartButtons();
        }
        else
        {
            StatusLabel();
        }
        GUILayout.EndArea();
    }

    static void StartButtons()
    {
        if (GUILayout.Button("Server + Client")) NetworkManager.Singleton.StartHost();
        if (GUILayout.Button("Client")) NetworkManager.Singleton.StartClient();
        if (GUILayout.Button("Server only")) NetworkManager.Singleton.StartServer();
    }

    static void StatusLabel()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        var mode = NetworkManager.Singleton.IsHost ? "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";
        GUILayout.Label("Mode: " + mode);
        if (NetworkManager.Singleton.IsServer)
            if (GUILayout.Button("Start Game"))
                GameManager.Singleton.StartGame();
        GUILayout.EndArea();
    }
}
