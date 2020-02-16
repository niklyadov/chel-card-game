using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetParametres : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("plus", false);
        animator.SetBool("minus", false);
    }
}
