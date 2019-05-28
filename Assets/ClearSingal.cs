using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearSingal : StateMachineBehaviour {
    public string[] ClearSingalname;
    public State thisState;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
     if(thisState==State.Enter)
        { 
                for (int i = 0; i < ClearSingalname.Length; i++)
                {
                    animator.ResetTrigger(ClearSingalname[i]);
                }
            
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (thisState == State.Stay)
        {
            for (int i = 0; i < ClearSingalname.Length; i++)
            {
                animator.ResetTrigger(ClearSingalname[i]);
            }

        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (thisState == State.Exit)
        {
            for (int i = 0; i < ClearSingalname.Length; i++)
            {
                animator.ResetTrigger(ClearSingalname[i]);
            }

        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

}
public enum State
{
    Enter,
    Stay,
    Exit
}