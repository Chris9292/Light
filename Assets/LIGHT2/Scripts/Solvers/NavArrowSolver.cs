using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;

public class NavArrowSolver : Solver
{
    public float up;
    public float forward;
    public override void SolverUpdate()
    {
        if (SolverHandler != null && SolverHandler.TransformTarget != null)
        {
            var target = SolverHandler.TransformTarget;

            // Keep the same position.y and the same rotation.x
            GoalPosition = target.position + target.rotation * Vector3.forward * forward + Vector3.up * up;
            //GoalRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, target.rotation.eulerAngles.y, target.rotation.eulerAngles.z);
        }
    }
}
