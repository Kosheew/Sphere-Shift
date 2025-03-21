using UnityEngine;
using Player;
using UnityEngine.Events;

public class VictoryDefeatManager : MonoBehaviour
{
    public UnityEvent OnVictory;
    public UnityEvent OnDefeat;
    
    private PlayerSizeHandler _playerSizeHandler;
    public PathClearanceHandler _pathClearanceHandler;
    private PlayerBall _playerBall;
    
    public void Inject(DependencyContainer container)
    {
        _playerSizeHandler = container.Resolve<PlayerSizeHandler>();
        _playerBall = container.Resolve<PlayerBall>();
        _pathClearanceHandler = container.Resolve<PathClearanceHandler>();
        
        _playerSizeHandler.OnSizeCompleted += HandlerDefeat;
        
        OnDefeat.AddListener(Defeat);
        OnVictory.AddListener(Victory);
    }

    private void Defeat()
    {
        _playerBall.DisableShooting();
        _pathClearanceHandler.IsPathClearance = false;
    }

    private void Victory()
    {
        _pathClearanceHandler.IsPathClearance = false;
    }

    private void HandlerDefeat()
    {
        OnDefeat?.Invoke();
    }
    
    public void Cleanup()
    {
        _playerSizeHandler.OnSizeCompleted -= HandlerDefeat;
    }
}
