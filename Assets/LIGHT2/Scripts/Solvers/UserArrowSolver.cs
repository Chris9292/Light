using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using UnityEngine;

public class UserArrowSolver : Solver
{
    public override void SolverUpdate()
    {
        if (SolverHandler != null && SolverHandler.TransformTarget != null)
        {
            var target = SolverHandler.TransformTarget;

            // Keep the same position.y and the same rotation.x
            GoalPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
            GoalRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, target.rotation.eulerAngles.y, target.rotation.eulerAngles.z);
        }
    }

}
