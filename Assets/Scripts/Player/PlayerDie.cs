using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    public bool isdead;

    [SerializeField] Animator animator;

    public void Die()
    {
        if (isdead) return;

        isdead = true;
        animator.SetTrigger("Die");
    }
}
