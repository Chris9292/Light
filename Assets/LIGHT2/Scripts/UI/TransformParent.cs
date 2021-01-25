using UnityEngine;

/// <summary>
/// This is a workaround since Transform.SetParent to None is bugged in TapToPlace events for Instantiated Objects 
/// </summary>

public class TransformParent : MonoBehaviour
{
    public void NoParent()
    {
        transform.parent = null;
    }
}
