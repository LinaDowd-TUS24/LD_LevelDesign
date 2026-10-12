using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimControl : MonoBehaviour
{
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    public void OnMove(InputValue value)
    {
      
        var moveVector = value.Get<Vector2>();

        Debug.Log(moveVector);

        if (moveVector.y > 0.1)
        {
            animator.SetFloat("MovingV", 1f);
        }
        else if (moveVector.y < -0.1)
        {
            animator.SetFloat("MovingV", -1f);
        }
        else
        {
            animator.SetFloat("MovingV", 0f);
        }

        if (moveVector.x > 0.1)
        {
            animator.SetFloat("MovingH", 1f);
        }
        else if (moveVector.x < -0.1)
        {
            animator.SetFloat("MovingH", -1f);
        }
        else
        {
            animator.SetFloat("MovingH", 0f);
        }
    }

    public void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            animator.SetTrigger("Attack");

            Debug.Log("attacking");
        }

    }

}
