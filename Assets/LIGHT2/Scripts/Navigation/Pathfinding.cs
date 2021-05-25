using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {

    public Transform a;
    public Transform b;
    public Transform c;
    public Transform startPosition;
    public Transform targetPosition;
    
    private NavigationGrid gridReference;
    private Path path;

    // Events
    public delegate void PathfindingEventHandler();
    public event PathfindingEventHandler OnPathCalculated;

    [Tooltip("Dynamic path update time")]
    public float DynamicPathUpdateTime = 0.3f;

    private void Awake()
    {
        gridReference = GetComponent<NavigationGrid>();
        path = GetComponent<Path>();
    }

    private void Start()
    {
        CalculateStaticPath();
        StartCoroutine(FindPath(startPosition, targetPosition, false));
    }

    // Reset the static line renderer and calculate path
    public void CalculateStaticPath()
    {
        // Reset static path
        path.staticPathRenderer.positionCount = 0;

        // Calculate static path
        StartCoroutine(FindPath(a, b, true));
        StartCoroutine(FindPath(b, c, true));
        StartCoroutine(FindPath(c, a, true));
    }

    private IEnumerator FindPath(Transform aObj, Transform bObj, bool isStatic)
    {
        while (true)
        {
            yield return new WaitForSeconds(DynamicPathUpdateTime);
            
            var aPosition = aObj.position;
            var bPosition = bObj.position;
            var startNode = gridReference.NodeFromWorldPoint(aPosition);
            var targetNode = gridReference.NodeFromWorldPoint(bPosition);

            // List of nodes for the open list
            var openList = new List<Node>();
            
            // Hashset of nodes for the closed list
            var closedList = new HashSet<Node>();

            // Add the starting node to the open list to start the algorithm
            openList.Add(startNode);
            
            // Whilst there is something in the open list
            while(openList.Count > 0)
            {
                // Create a node and set it to the first item in the open list
                var currentNode = openList[0];
                
                // Loop through the open list starting from the second object
                for(var i = 1; i < openList.Count; i++)
                {
                    // If the f cost of that object is less than or equal to the f cost of the current node
                    if (openList[i].FCost < currentNode.FCost || openList[i].FCost == currentNode.FCost && openList[i].ihCost < currentNode.ihCost)
                    {
                        // Set the current node to that object
                        currentNode = openList[i];
                    }
                }
                
                // Remove it from the open list and add it to the closed list
                openList.Remove(currentNode);
                closedList.Add(currentNode);

                // If the current node is the same as the target node
                if (currentNode == targetNode)
                {
                    // Calculate the final path
                    GetFinalPath(startNode, targetNode, isStatic);
                }

                // Loop through each neighbor of the current node
                foreach (var neighborNode in gridReference.GetNeighboringNodes(currentNode))
                {
                    // If the neighbor is a wall or has already been checked
                    if (neighborNode.isObstructed || closedList.Contains(neighborNode))
                    {
                        // Skip it
                        continue;
                    }
                    
                    // Get the F cost of that neighbor
                    var moveCost = currentNode.igCost + GetManhattanDistance(currentNode, neighborNode);

                    if (!(moveCost < neighborNode.igCost || !openList.Contains(neighborNode)))
                    {
                        // Skip it
                        continue;
                    }

                    // Set the g cost to the f cost
                    neighborNode.igCost = moveCost;
                    
                    // Set the h cost
                    neighborNode.ihCost = GetManhattanDistance(neighborNode, targetNode);
                    
                    // Set the parent of the node for retracing steps
                    neighborNode.ParentNode = currentNode;

                    // If the neighbor is not in the openList
                    if(!openList.Contains(neighborNode))
                    {
                        // Add it to the list
                        openList.Add(neighborNode);
                    }
                }
            }

            if (isStatic)
            {
                break;
            }
        }  
    }

    private void GetFinalPath(Node aStartingNode, Node aEndNode, bool isStatic)
    {
        // List to hold the path sequentially 
        var finalPath = new List<Node>();
        
        // Node to store the current node being checked
        var currentNode = aEndNode;

        // While loop to work through each node going through the parents to the beginning of the path
        while(currentNode != aStartingNode)
        {
            // Add that node to the final path
            finalPath.Add(currentNode);
            
            // Move onto its parent node
            currentNode = currentNode.ParentNode;
        }
        
        // Reverse the path to get the correct order
        finalPath.Reverse();
        
        // Set the final path
        gridReference.FinalPath = finalPath;
        
        // static path / dynamic path
        if (isStatic)
        {
            path.CreateStaticPathObjects();
        }
        else
        {
            path.UpdateDynamicPathObjects();
            OnPathCalculated?.Invoke();
        }
    }
    
    private static int GetManhattanDistance(Node nodeA, Node nodeB)
    {
        // calculates h cost: (x1 - x2) + (y1 - y2)
        
        var ix = Mathf.Abs(nodeA.iGridX - nodeB.iGridX);
        var iy = Mathf.Abs(nodeA.iGridY - nodeB.iGridY);
        return ix + iy;
    }
}
