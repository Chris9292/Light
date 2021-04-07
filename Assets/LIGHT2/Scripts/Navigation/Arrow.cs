using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // arrow object that points to the nearest node on the path
    private GameObject objectToLookAt;
    Pathfinding pathfinding;

    void Awake()
    {
        pathfinding = GameObject.FindGameObjectWithTag("Navigation").GetComponent<Pathfinding>();
    }
    private void Start()
    {
        pathfinding.OnPathCalculated += UpdateArrow;
    }
    
    public void UpdateArrow()
    {
        // Efaptomenh metaksy [0] kai [1]
        Vector3 direction = ObjectPool.SharedInstance.pooledObjects[1].transform.position - ObjectPool.SharedInstance.pooledObjects[0].transform.position;
        direction = direction.normalized;

        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        
        // Update the orientation of the arrow to the nearest path object
        // while (true)
        // {
        //     yield return new WaitForSeconds(2.0f);
        //objectToLookAt = ObjectPool.SharedInstance.pooledObjects[2];
        //transform.LookAt(objectToLookAt.transform);
        //}
    }
}
