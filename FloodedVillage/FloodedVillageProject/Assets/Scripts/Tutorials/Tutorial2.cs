using UnityEngine;

public class Tutorial2 : MonoBehaviour
{
    [SerializeField] Animator _animator;

    private void OnMouseDown()
    {
        _animator.SetTrigger("Tuto2");
    }
}
