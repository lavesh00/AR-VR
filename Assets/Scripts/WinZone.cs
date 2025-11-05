using UnityEngine;

public class WinZone : MonoBehaviour
{
    private Renderer zoneRenderer;
    private Player player;
    private bool isFullyParked = false;
    
    void Start()
    {
        zoneRenderer = GetComponent<Renderer>();
        if (zoneRenderer != null)
        {
            zoneRenderer.material.color = Color.red;
        }
        player = FindObjectOfType<Player>();
        
        Collider currentCollider = GetComponent<Collider>();
        
        // If there's a MeshCollider, replace it with a BoxCollider
        MeshCollider meshCollider = currentCollider as MeshCollider;
        if (meshCollider != null)
        {
            Bounds boundsToUse;
            if (zoneRenderer != null)
            {
                // Use renderer bounds for visual accuracy
                boundsToUse = zoneRenderer.bounds;
            }
            else
            {
                // Fallback to mesh collider bounds
                boundsToUse = meshCollider.bounds;
            }
            
            DestroyImmediate(meshCollider); // Remove the MeshCollider
            
            BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
            boxCollider.size = boundsToUse.size;
            boxCollider.center = boundsToUse.center - transform.position;
            currentCollider = boxCollider; // Update currentCollider reference
        }
        
        // Ensure the final collider is a trigger
        if (currentCollider != null)
        {
            currentCollider.isTrigger = true;
        }
    }
    
    void Update()
    {
        if (player != null && !isFullyParked)
        {
            CheckIfFullyParked();
        }
    }
    
    private void CheckIfFullyParked()
    {
        // Get all wheel positions
        Vector3 frontLeftPos = player.frontLeftWheelTransform.position;
        Vector3 frontRightPos = player.frontRightWheelTransform.position;
        Vector3 rearLeftPos = player.rearLeftWheelTransform.position;
        Vector3 rearRightPos = player.rearRightWheelTransform.position;
        
        // Check if all wheels are inside the win zone
        bool frontLeftInside = IsPointInsideZone(frontLeftPos);
        bool frontRightInside = IsPointInsideZone(frontRightPos);
        bool rearLeftInside = IsPointInsideZone(rearLeftPos);
        bool rearRightInside = IsPointInsideZone(rearRightPos);
        
        // Check if car is straight (aligned with the zone)
        bool isStraight = IsCarStraight();
        
        // Debug info
        Debug.Log($"Wheel positions - FL:{frontLeftInside} FR:{frontRightInside} RL:{rearLeftInside} RR:{rearRightInside}");
        Debug.Log($"Car is straight: {isStraight}");
        
        if (frontLeftInside && frontRightInside && rearLeftInside && rearRightInside && isStraight)
        {
            if (!isFullyParked)
            {
                Debug.Log("All wheels inside and car is straight! Triggering win!");
                ChangeToGreen();
                player.ToggleWin();
                isFullyParked = true;
            }
        }
    }
    
    private bool IsPointInsideZone(Vector3 point)
    {
        // Use renderer bounds instead of collider bounds for accurate detection
        if (zoneRenderer != null)
        {
            Bounds bounds = zoneRenderer.bounds;
            
            // Debug the bounds and point
            Debug.Log($"Renderer bounds: {bounds.min} to {bounds.max}, Point: {point}");
            
            // Check if point is inside bounds (ignore Y for floating zones)
            bool insideX = point.x >= bounds.min.x && point.x <= bounds.max.x;
            bool insideZ = point.z >= bounds.min.z && point.z <= bounds.max.z;
            
            Debug.Log($"Inside X: {insideX}, Inside Z: {insideZ}");
            
            return insideX && insideZ;
        }
        return false;
    }
    
    private bool IsCarStraight()
    {
        // Get car's rotation
        float carRotationY = player.transform.eulerAngles.y;
        
        // Normalize rotation to 0-360 range
        carRotationY = carRotationY % 360;
        if (carRotationY < 0) carRotationY += 360;
        
        // Check if car is roughly straight (within 10 degrees of 0, 90, 180, or 270 degrees)
        bool isStraight = (carRotationY <= 10 || carRotationY >= 350) || // 0 degrees
                         (carRotationY >= 80 && carRotationY <= 100) ||   // 90 degrees
                         (carRotationY >= 170 && carRotationY <= 190) ||   // 180 degrees
                         (carRotationY >= 260 && carRotationY <= 280);    // 270 degrees
        
        Debug.Log($"Car rotation Y: {carRotationY}, Is straight: {isStraight}");
        
        return isStraight;
    }
    
    public void ChangeToGreen()
    {
        if (zoneRenderer != null)
        {
            zoneRenderer.material.color = Color.green;
        }
    }
}
