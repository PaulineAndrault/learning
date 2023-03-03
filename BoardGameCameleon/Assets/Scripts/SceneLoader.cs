using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : NetworkBehaviour
{
    public void LoadNextScene()
    {
        //string nextScene = SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1).name;
        NetworkManager.SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
