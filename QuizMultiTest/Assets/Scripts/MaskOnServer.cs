using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MaskOnServer : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        gameObject.SetActive(NetworkManager.Singleton.IsServer ? false : true);
    }
}
