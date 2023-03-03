using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MaskOnClients : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        gameObject.SetActive(NetworkManager.Singleton.IsServer ? true : false);
    }
}
