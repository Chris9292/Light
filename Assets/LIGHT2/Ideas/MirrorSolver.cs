using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;

public class MirrorSolver : Solver
{
    // The rotation which is applied on the object's movement in reference to the tracked object.
    public Vector3 MirrorRotation = Vector3.zero;
    // The point from where the move gets mirrored.
    private Vector3 ReferencePoint;
    // The travelled distance is multiplied by this number. Negative numbers may be used to change the direction.
    public float DistanceAmplifier = 1f;
    private bool hasReferencePoint = false;

    
    // Sets the reference point as the current location of the tracked target.
    private void UpdateReferencePoint()
    {
        if (SolverHandler != null && SolverHandler.TransformTarget != null)
        {
            ReferencePoint = SolverHandler.TransformTarget.position;
        }
    }

    public override void SolverUpdate()
    {
        if (SolverHandler != null && SolverHandler.TransformTarget != null)
        {
            if (!hasReferencePoint)
            {
                UpdateReferencePoint();
                hasReferencePoint = true;
            }
            var target = SolverHandler.TransformTarget;
            // Math
            GoalPosition = Quaternion.Euler(MirrorRotation) * (target.position - ReferencePoint) * DistanceAmplifier + ReferencePoint;

        }
    }

    private void OnDisable()
    {
        hasReferencePoint = false;
    }
}
