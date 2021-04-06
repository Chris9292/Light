using UnityEngine;

public class Node
{
    // Represents the nodes within the grid
    
    // X, Y Position in the Node Array
    public readonly int iGridX;
    public readonly int iGridY;

    // node is obstructed
    public readonly bool isObstructed;
    
    // The world position of the node
    public Vector3 vPosition;

    // For the A-Star algorithm, will store what node it previously came from so it can trace the shortest path
    public Node ParentNode;
    
    // The cost of moving to the next square
    public int igCost;
    
    // The distance to the goal from this node
    public int ihCost;

    public int FCost => igCost + ihCost;

    public Node(bool aIsObstructed, Vector3 aVPos, int aGridX, int aGridY)//Constructor
    {
        isObstructed = aIsObstructed;
        vPosition = aVPos;
        iGridX = aGridX;
        iGridY = aGridY;
    }
}
