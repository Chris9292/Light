using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraManipulator : ObjectManipulator
{
    public Vector3 RotatedManipulation = new Vector3(90f, 0f, 0f);
    protected void ApplyTargetTransform(MixedRealityTransform targetTransform)
    {
        //targetTransform.Position = Quaternion.Euler(RotatedManipulation) * targetTransform.Position;
        //HostTransform.position = SmoothTo(HostTransform.position, targetTransform.Position, moveLerpTime);
        Debug.Log("I was overriden!");

    }
}
