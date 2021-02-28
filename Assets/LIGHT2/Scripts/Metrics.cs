using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metrics : MonoBehaviour
{
    Grid GridReference;
    private float distance;
    private float slope;
    private float velocity;
    private float MetabolicRate;
    private void Awake()
    {
      //GridReference = GetComponent<Grid>;  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //float GetWorldDist(float x ,float y){


   // }

    int GetManhattanDist(Node nodeA,Node nodeB){
        int ix = Mathf.Abs(nodeA.iGridX - nodeB.iGridX);//x1-x2
        int iy = Mathf.Abs(nodeA.iGridY - nodeB.iGridY);//y1-y2

        return ix + iy;//Return the sum
    }
    
}
