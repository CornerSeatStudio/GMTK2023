using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : SpeedModifyZone
{

    public float speedForce;

    protected override void DoTheMagic(BallMovement ball)
    {
        Vector3 currDir =  ball.rb.velocity.normalized;
        ball.rb.AddForce(Vector3.up * speedForce, ForceMode.Impulse); //CHANGE THIS
    }
}
