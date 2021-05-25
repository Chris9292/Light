using System.Collections.Generic;
using UnityEngine;

public class NavigationGrid : MonoBehaviour
{
    public LayerMask wallMask;      // This is the mask that the program will look for when trying to find obstructions to the path
    public Vector2 vGridWorldSize;  // A vector2 to store the width and height of the graph in world units
    public List<Node> FinalPath;    // The completed path that the red line will be drawn along
    public float fNodeRadius;       // This stores how big each square on the graph will be
    
    private Node[,] nodeArray;      // The array of nodes that the A Star algorithm uses
    private float fNodeDiameter;    // Twice the amount of the radius (Set in the start function)
    private int iGridSizeX;         // Size of the Grid in Array units
    private int iGridSizeY;         // Size of the Grid in Array units

    private void Start()
    {
        Init();
    }

    // Initialize Grid
    public void Init()
    {
        // Double the radius to get diameter
        fNodeDiameter = fNodeRadius * 2;

        // Divide the grids world coordinates by the diameter to get the size of the graph in array units
        iGridSizeX = Mathf.RoundToInt(vGridWorldSize.x / fNodeDiameter);
        iGridSizeY = Mathf.RoundToInt(vGridWorldSize.y / fNodeDiameter);
        CreateGrid();
    }
    
    private void CreateGrid()
    {
        // Declare the array of nodes
        nodeArray = new Node[iGridSizeX, iGridSizeY];
        
        //Get the real world position of the bottom left of the grid
        var bottomLeft = transform.position - Vector3.right * vGridWorldSize.x / 2 - Vector3.forward * vGridWorldSize.y / 2;
        
        // Loop through the array of nodes
        for (var x = 0; x < iGridSizeX; x++)
        {
            for (var y = 0; y < iGridSizeY; y++)
            {
                // Get the world coordinates of the bottom left of the graph
                // Check if the node is being obscured
                var worldPoint = bottomLeft + Vector3.right * (x * fNodeDiameter + fNodeRadius) + Vector3.forward * (y * fNodeDiameter + fNodeRadius);
                var obstructed = Physics.CheckSphere(worldPoint, fNodeRadius, wallMask);
                
                /*
                 Quick collision check against the current node and anything in the world at its position
                 If it is colliding with an object of layer : [wallMask], the if statement will return true
                */
                
                // Create a new node in the array
                nodeArray[x, y] = new Node(obstructed, worldPoint, x, y);
            }
        }
    }
    
    public List<Node> GetNeighboringNodes(Node aNeighborNode)
    {
        // Function that gets the neighboring nodes of the given node
        
        // Make a new list of all available neighbors
        var neighborList = new List<Node>();
        
        // Check for neighbors (max 8 neighbors)
        for (var x = -1; x <= 1; x++)
        {
            for (var y = -1; y <= 1; y++)
            {
                // if we are on the node tha was passed in, skip this iteration
                if (x == 0 && y == 0) {
                    continue;
                }

                var checkX = aNeighborNode.iGridX + x;
                var checkY = aNeighborNode.iGridY + y;

                // Make sure the node is within the grid
                if (checkX >= 0 && checkX < iGridSizeX && checkY >= 0 && checkY < iGridSizeY)
                {
                    // Add node to the neighbor list
                    neighborList.Add(nodeArray[checkX, checkY]); 
                }
            }
        }
        return neighborList;
    }

    public Node NodeFromWorldPoint(Vector3 aVectorWorldPos)
    {
        // Gets the closest node to the given world position
        var ixPos = ((aVectorWorldPos.x + vGridWorldSize.x / 2) / vGridWorldSize.x);
        var iyPos = ((aVectorWorldPos.z + vGridWorldSize.y / 2) / vGridWorldSize.y);
        ixPos = Mathf.Clamp01(ixPos);
        iyPos = Mathf.Clamp01(iyPos);
        var ix = Mathf.RoundToInt((iGridSizeX - 1) * ixPos);
        var iy = Mathf.RoundToInt((iGridSizeY - 1) * iyPos);
        return nodeArray[ix, iy];
    }
        //Function that draws the wireframe
    /*private void OnDrawGizmos()
    {

        Gizmos.DrawWireCube(transform.position, new Vector3(vGridWorldSize.x, 1, vGridWorldSize.y));//Draw a wire cube with the given dimensions from the Unity inspector

        if (nodeArray != null)//If the grid is not empty
        {
            foreach (Node n in nodeArray)//Loop through every node in the grid
            {
                if (n.isObstructed)//If the current node is a wall node
                {
                    Gizmos.color = Color.white;//Set the color of the node
                }
                else
                {
                    Gizmos.color = Color.yellow;//Set the color of the node
                }


                if (FinalPath != null)//If the final path is not empty
                {
                    if (FinalPath.Contains(n))//If the current node is in the final path
                    {
                        Gizmos.color = Color.red;//Set the color of that node
                    }

                }
                Gizmos.DrawCube(n.vPosition, Vector3.one * (fNodeDiameter));//Draw the node at the position of the node.
                
            }
        }
    }
    */
}

