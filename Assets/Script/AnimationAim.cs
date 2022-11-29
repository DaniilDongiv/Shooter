using UnityEngine;

public class AnimationAim : MonoBehaviour
{
    private Animator _animator;
    private static readonly int _shot = Animator.StringToHash("shot");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void AnimAim()
    {
        _animator.SetTrigger(_shot);
    }
}
