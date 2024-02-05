using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Wanderer : Kinematic
{
    Wander myMoveType;

    void Start()
    {
        myMoveType = new Wander();
        myMoveType.character = this;
    }

    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.linear = myMoveType.getSteering().linear;
        steeringUpdate.angular = myMoveType.getSteering().angular;
        base.Update();
    }
}