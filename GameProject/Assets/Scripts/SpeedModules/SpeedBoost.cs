using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : SpeedModifyZone
{
    public float spinForce = 1f;
    public float speedForce;

    protected override void DoTheMagic(BallMovement ball)
    {
        Vector3 torqueDirection = transform.right;
        Vector3 currDir =  ball.rb.velocity.normalized;
        ball.rb.AddForce(Vector3.forward * speedForce, ForceMode.Impulse);
        ball.rb.AddTorque(torqueDirection * spinForce, ForceMode.Impulse);
    }
}
