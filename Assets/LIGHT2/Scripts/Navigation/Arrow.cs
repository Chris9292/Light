using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // arrow object that points to the nearest node on the path
    private GameObject objectToLookAt;

    private void Start()
    {
        StartCoroutine(UpdateArrowOrientation());
    }
    
    private IEnumerator UpdateArrowOrientation()
    {
        // Update the orientation of the arrow to the nearest path object
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            objectToLookAt = ObjectPool.SharedInstance.pooledObjects[0];
            transform.LookAt(objectToLookAt.transform);
        }
    }
}
