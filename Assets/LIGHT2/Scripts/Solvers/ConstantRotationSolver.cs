using UnityEngine;

/// <summary>
/// Keeps the rotation constant
/// </summary>

public class ConstantRotationSolver : MonoBehaviour
{
    public Quaternion rotation;

    private void Awake()
    {
        rotation = transform.rotation;
    }

    void Update()
    {
        transform.rotation = rotation;
    }
}
