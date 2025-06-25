using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatios : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    
    public void ShootAnimation()
    {
        playerAnimator.SetTrigger("ShootTrigger");
    }

    public void MotionAnimation(float motion)
    {
        playerAnimator.SetFloat("SpeedMotion",motion);
    }

    public void SetOnAirTrue()
    {
        playerAnimator.SetBool("IsOnAir", true);
    }
    public void SetOnAirFalse()
    {
        playerAnimator.SetBool("IsOnAir", false);
    }
    public void SetYSpeed(float ySpeed)
    {
        playerAnimator.SetFloat("YSpeed", ySpeed);
    }
}
