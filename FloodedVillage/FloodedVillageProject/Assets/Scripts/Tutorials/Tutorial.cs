using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{

    [SerializeField] Animator _animator;

    private void OnMouseDown()
    {
        if(SceneManager.GetActiveScene().name == "Level3")
        {
            if(Input.GetKey("space"))
            {
                _animator.SetTrigger("Tuto1");
            }
        }
        else
        {
            _animator.SetTrigger("Tuto1");
        }
    }
}
