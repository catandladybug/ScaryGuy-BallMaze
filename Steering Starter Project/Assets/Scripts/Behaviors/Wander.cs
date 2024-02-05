using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Wander : SteeringBehavior
{
    public Kinematic character;

    float wanderOffset = 1f;
    float wanderRadius = 1f;

    float wanderRate = 1f;

    float wanderOrientation;

    float wanderAcceleration = 3f;

    Face face = new();

    public override SteeringOutput getSteering()
    {
        SteeringOutput output = new SteeringOutput();

        wanderOrientation += RandomBinomial() * wanderRate * Mathf.Rad2Deg;

        float targetOrientation = wanderOrientation + character.transform.rotation.eulerAngles.y;

        Vector3 targetPos = character.transform.position + (wanderOffset * Vector3.one) + character.transform.forward;

        targetPos += wanderRadius * targetOrientation * Vector3.one;

        output.angular = FaceTarget(targetPos).angular;

        output.linear = wanderAcceleration * character.transform.forward;

        return output;
    }

    public float RandomBinomial()
    {
        return Random.value - Random.value;
    }
    public float getTargetAngle(Vector3 target)
    {
        Vector3 direction = character.transform.position - target;
        float targetAngle = Mathf.Atan2(-direction.x, direction.z) * Mathf.Rad2Deg;

        return targetAngle;
    }

    float maxAngularAcceleration = 100f;
    float maxRotation = 45f;

    float slowRadius = 10f;

    float timeToTarget = 0.1f;

    public SteeringOutput FaceTarget(Vector3 target)
    {
        SteeringOutput result = new SteeringOutput();

        float rotation = Mathf.DeltaAngle(character.transform.eulerAngles.y, getTargetAngle(target));
        float rotationSize = Mathf.Abs(rotation);


        float targetRotation = 0.0f;
        if (rotationSize > slowRadius)
        {
            targetRotation = maxRotation;
        }
        else
        {
            targetRotation = maxRotation * rotationSize / slowRadius;
        }

        targetRotation *= rotation / rotationSize;

        float currentAngularVelocity = float.IsNaN(character.angularVelocity) ? 0f : character.angularVelocity;
        result.angular = targetRotation - currentAngularVelocity;
        result.angular /= timeToTarget;

        float angularAcceleration = Mathf.Abs(result.angular);
        if (angularAcceleration > maxAngularAcceleration)
        {
            result.angular /= angularAcceleration;
            result.angular *= maxAngularAcceleration;
        }

        result.linear = Vector3.zero;
        return result;
    }
}
