using UnityEngine;
using Player;
using Environment;

public class Game : MonoBehaviour
{
    [Header("FPS Application")] 
    [SerializeField] private int targetFPS = 30;
    
    [Header("Player")]
    [SerializeField] private PlayerBall playerBall;
    [SerializeField] private PlayerSizeHandler playerSizeHandler;
    [SerializeField] private PlayerMovement playerMovement;
    
    [Header("Props")]
    [SerializeField] private Road road;
    [SerializeField] private Door door;
    
    [Header("Managers")]
    [SerializeField] private ProjectileManager projectileManager;
    [SerializeField] private VictoryDefeatManager victoryDefeatManager;
    [SerializeField] private PathClearanceHandler clearanceHandler;
    private DependencyContainer _container;

    private InputHandler _inputHandler;

    private void Awake()
    {
        Application.targetFrameRate = targetFPS;
        
        _container = new DependencyContainer();
        _inputHandler = new InputHandler();
        
        Register();
        
        Initialize();
    }

    private void Update()
    {
        _inputHandler.UpdateHandler();
        clearanceHandler.UpdateHandler();
    }
    
    private void Register()
    {
        _container.Register(_inputHandler);
        
        // register Player
        _container.Register(playerBall);
        _container.Register(playerSizeHandler);
        _container.Register(playerMovement);
        
        _container.Register(road);
        _container.Register(victoryDefeatManager);
        _container.Register(projectileManager);
        _container.Register(clearanceHandler);
    }

    private void Initialize()
    {
        playerBall.Inject(_container);
        playerMovement.Initialize();
        playerSizeHandler.Initialize();
        
        door.Inject(_container);
        road.Inject(_container);
        
        projectileManager.Inject(_container);
        victoryDefeatManager.Inject(_container);
        clearanceHandler.Inject(_container);
    }

    private void OnDestroy()
    {
        playerBall.Cleanup();
        victoryDefeatManager.Cleanup();
    }
}
