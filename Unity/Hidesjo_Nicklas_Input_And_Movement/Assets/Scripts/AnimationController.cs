using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AnimationController : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    void Update()
    {
        
    }
    public void setAnimation(string animation)
    {
        switch(animation)
        {
            case "WalkUp":
                if (animator.GetBool("walkUp") == true) { return; }
                animator.SetBool("idle", false);
                animator.SetBool("walkLeft", false);
                animator.SetBool("walkDown", false);
                animator.SetBool("walkRight", false);
                animator.SetBool("walkUp", true);
                break;
            case "WalkRight":
                if (animator.GetBool("walkRight") == true) { return; }
                animator.SetBool("idle", false);
                animator.SetBool("walkLeft", false);
                animator.SetBool("walkDown", false);
                animator.SetBool("walkUp", false);
                animator.SetBool("walkRight", true);
                break;
            case "WalkDown":
                if (animator.GetBool("walkDown") == true) { return; }
                animator.SetBool("idle", false);
                animator.SetBool("walkLeft", false);
                animator.SetBool("walkUp", false);
                animator.SetBool("walkRight", false);
                animator.SetBool("walkDown", true);
                break;
            case "WalkLeft":
                if(animator.GetBool("walkLeft") == true) { return; }
                animator.SetBool("idle", false);
                animator.SetBool("walkUp", false);
                animator.SetBool("walkRight", false);
                animator.SetBool("walkDown", false);
                animator.SetBool("walkLeft", true);
                break;
            case "Idle":
                if (animator.GetBool("idle") == true) { return; }
                else
                {
                    animator.SetBool("walkLeft", false);
                    animator.SetBool("walkUp", false);
                    animator.SetBool("walkRight", false);
                    animator.SetBool("walkDown", false);
                    animator.SetBool("idle", true);
                    break;
                }
        }

    }

}
