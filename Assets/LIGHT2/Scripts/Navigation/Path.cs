using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Transform startPosition;
    public LineRenderer pathRenderer;
    public GameObject pathObject;
    public GameObject arrow;
    
    private NavigationGrid gridReference;

    // Holograms hidden from menus
    Transform holograms;

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
        foreach (var n in gridReference.FinalPath)
        {
            Instantiate(pathObject, n.vPosition, Quaternion.identity);
        }
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
        pathRenderer.positionCount = gridReference.FinalPath.Count + 1;
        pathRenderer.SetPosition(0, startPosition.position);
        foreach (var node in gridReference.FinalPath)
        {
            pathRenderer.SetPosition(count, node.vPosition);
            count++;
        }
    }
}
