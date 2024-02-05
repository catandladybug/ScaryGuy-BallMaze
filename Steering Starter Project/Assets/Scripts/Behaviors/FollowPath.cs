using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : Seek
{

    public GameObject[] targets;

    public float threshold = 0.5f;
    float decayCoefficient = 100f;
    float maxAcceleration = 0.5f;

    int index = 0;
    protected override Vector3 getTargetPosition()
    {

        foreach (GameObject target in targets)
        {
            Vector3 directionToTarget = targets[index].transform.position - character.transform.position;
            float distanceToTarget = directionToTarget.magnitude;

            if (distanceToTarget < threshold)
            {
                index++;

                if (index >= targets.Length)
                {
                    index = 0;
                }
            }
        }

        return targets[index].transform.position;

    }
}