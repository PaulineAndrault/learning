using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    // States used by Clients :
    ClientLocked,
    ClientUnlocked,
    // States used by Server :
    ServerTurn,
    ClientTurn
}

[System.Serializable]
public class Team
{
    public ulong ClientId;
    public string TeamName;
    public int Score;
    public Color Color;

    public Team(ulong id, string name)
    {
        ClientId = id;
        TeamName = name;
        Score = 0;
        switch (id)
        {
            case 1:
                Color = Color.blue;
                break;
            case 2:
                Color = Color.red;
                break;
            case 3:
                Color = Color.yellow;
                break;
            case 4:
                Color = Color.green;
                break;
        }
    }
}

public class GameManager : NetworkBehaviour
{
    public GameState CurrentState;

    // Server vars
    public List<Team> Teams = new List<Team>();

    // Client vars
    public Team TeamInfo;

    #region Singleton pattern
    public static GameManager Singleton;
    private void Awake()
    {
        if (Singleton != null)
            Destroy(this);

        Singleton = this;
        DontDestroyOnLoad(this);
    }
    #endregion

    private void OnEnable()
    {
        SceneManager.sceneLoaded += InitializeScene;
    }

    private void InitializeScene(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Game")
        {

        }
    }

    
}
