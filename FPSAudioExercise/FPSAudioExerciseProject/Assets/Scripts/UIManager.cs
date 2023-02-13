using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _worldSpaceCanvas;

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            _worldSpaceCanvas.SetActive(_worldSpaceCanvas.activeInHierarchy ? false : true);
    }
}
