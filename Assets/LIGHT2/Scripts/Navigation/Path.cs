using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Transform startPosition;
    public LineRenderer dynamicPathRenderer;
    public LineRenderer staticPathRenderer;
    public GameObject pathObject;
    public GameObject arrow;

    private NavigationGrid gridReference;

    // Holograms hidden from menus
    private Transform holograms;

    //Events
    public delegate void LineUpdatedHandler(LineRenderer lineRenderer);
    public event LineUpdatedHandler OnStaticLineUpdated;
    public event LineUpdatedHandler OnDynamicLineUpdated;

    private void Start()
    {
        // Instantiate arrow in holograms
        holograms = GameObject.FindGameObjectWithTag("Holograms").transform;
        Instantiate(arrow, holograms);
        gridReference = GetComponent<NavigationGrid>();
    }

    public void CreateStaticPathObjects()
    {
        // Function that initializes objects
        if (gridReference.FinalPath.Count == 0)
        {
            return;
        }
        
        var currentPosition = staticPathRenderer.positionCount;
        staticPathRenderer.positionCount += gridReference.FinalPath.Count;
        foreach (var n in gridReference.FinalPath)
        {
            Instantiate(pathObject, n.vPosition, Quaternion.identity);
            staticPathRenderer.SetPosition(currentPosition, n.vPosition);
            currentPosition++;
        }

        // Call Events
        OnStaticLineUpdated?.Invoke(staticPathRenderer);
    }
    
    public void UpdateDynamicPathObjects()
    {
        // Function that activates objects on FinalPath. The user should follow the objects.
        ObjectPool.SharedInstance.DeactivatePoolObjects();
        if (gridReference.FinalPath != null)
        {
            foreach (var node in gridReference.FinalPath)
            {
                var pooledObject = ObjectPool.SharedInstance.GetPooledObject();
                if (!pooledObject)
                {
                    return;
                }
                pooledObject.transform.position = node.vPosition;
                pooledObject.transform.rotation = Quaternion.identity;
                pooledObject.SetActive(true);
            }
        }
        UpdateLineRenderer();
    }

    private void UpdateLineRenderer()
    {
        var count = 1;
        
        // +1 for the starting position = player position
        dynamicPathRenderer.positionCount = gridReference.FinalPath.Count + 1;
        dynamicPathRenderer.SetPosition(0, startPosition.position);
        foreach (var node in gridReference.FinalPath)
        {
            dynamicPathRenderer.SetPosition(count, node.vPosition);
            count++;
        }

        // Call Events
        OnDynamicLineUpdated?.Invoke(dynamicPathRenderer);
    }
}
