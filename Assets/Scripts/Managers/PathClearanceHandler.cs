using Player;
using UnityEngine;

public class PathClearanceHandler : MonoBehaviour
{
    [SerializeField] private float raycastDistance = 5f;  
    [SerializeField] private LayerMask obstacleLayer;    

    private PlayerMovement _playerMovement;
    private PlayerBall _playerBall;
    
    public bool IsPathClearance { get; set; }
    
    public void Inject(DependencyContainer container)
    {
        _playerMovement = container.Resolve<PlayerMovement>();
        _playerBall = container.Resolve<PlayerBall>();
        
        IsPathClearance = true;
    }
    
    public void UpdateHandler()
    {
        if(!IsPathClearance) return;
        
        float sizeOffset = _playerMovement.transform.localScale.x / 2;
        
        if (IsPathClear(sizeOffset) && IsPathClear(-sizeOffset) && IsPathClear(0))
        {
           _playerMovement.Move();
           _playerMovement.Jump();
           _playerBall.DisableShooting();
        }
    }

    private bool IsPathClear(float offsetRange)
    {
        Vector3 offset = new Vector3(offsetRange, 0, 0); 
        
        if (Physics.Raycast(transform.position + offset, transform.forward, out var hit, raycastDistance, obstacleLayer))
        {
            Debug.DrawRay(transform.position + offset, transform.forward * hit.distance, Color.yellow);
            return false;
        }
        return true;  
    }
}
