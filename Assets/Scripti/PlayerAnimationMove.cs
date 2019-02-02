using UnityEngine;
using System.Collections;

public class PlayerAnimationMove : MonoBehaviour
{
    public Animator animator;

	void Start ()
    {
         animator.GetComponent<Animator>();
	}

	void Update ()
    {
        var arrowkey = animator.GetBool("ArrowKey");
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        { 
            animator.SetBool("ArrowKey", true);
        }
        else
        {
            animator.SetBool("ArrowKey", false);
        }
	}
}
