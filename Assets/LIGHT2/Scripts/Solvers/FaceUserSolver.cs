using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using UnityEngine;

public class FaceUserSolver : Solver
{
    public override void SolverUpdate()
    {
        if (SolverHandler != null && SolverHandler.TransformTarget != null)
        {
            var target = SolverHandler.TransformTarget;

            Vector3 relativePos = transform.position - target.position;

            GoalPosition = transform.position;
            GoalRotation = Quaternion.LookRotation(relativePos, Vector3.up);
        }
    }
}
