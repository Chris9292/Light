using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    public LayerMask hitLayers;
    private void Update()
    {
        // If the player has left clicked
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }
        
        // Get the mouse Position
        var mouse = Input.mousePosition;
        
        // Cast a ray to get where the mouse is pointing at
        var castPoint = Camera.main.ScreenPointToRay(mouse);
        
        // Stores the position where the ray hit
        RaycastHit hit;
        
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, hitLayers))
        {
            // If the raycast doesnt hit a wall, move the target to the mouse position
            transform.position = hit.point;
        }
    }
}
