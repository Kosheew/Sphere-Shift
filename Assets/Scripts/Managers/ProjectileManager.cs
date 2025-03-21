using UnityEngine;
using ObjectPool;
using Player;
using Projectiles;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private float startSizeProjectile = 0.2f;
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] private int sizePool;
    [SerializeField] private Transform firePosition;
    
    private CustomPool<Projectile> _projectilePool;
    private Projectile _currentProjectile;
    private PlayerSizeHandler _playerSizeHandler;
    
    public void Inject(DependencyContainer container)
    {
        _projectilePool = new CustomPool<Projectile>(projectilePrefab, sizePool);
        _playerSizeHandler = container.Resolve<PlayerSizeHandler>();

        _playerSizeHandler.OnChargeChange += UpdateProjectile;
    }

    public void GetProjectile()
    {
        _currentProjectile = _projectilePool.Get();
        
        _currentProjectile.OnDestroyed += ResetPool;
        
        _currentProjectile.transform.position = firePosition.position;
        _currentProjectile.SetSize(startSizeProjectile); 
    }

    public void UpdateProjectile(float chargeTime)
    {
        if (_currentProjectile == null) return;
        
        _currentProjectile.SetSize(chargeTime);
    }

    public void LaunchProjectile()
    {
        if (_currentProjectile == null) return;
        
        _currentProjectile.Launch();
        _currentProjectile = null;
    }

    private void ResetPool(Projectile projectile)
    {
        projectile.OnDestroyed -= ResetPool;
        _projectilePool.Release(projectile);
    }
}
