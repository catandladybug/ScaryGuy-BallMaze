using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoider : Kinematic
{
    CollisionAvoidance myMoveType;

    public Kinematic[] targets = new Kinematic[4];

    void Start()
    {
        myMoveType = new CollisionAvoidance();
        myMoveType.character = this;
        myMoveType.targets = targets;
    }

    protected override void Update()
    {
        steeringUpdate = myMoveType.getSteering();
        base.Update();
    }
}