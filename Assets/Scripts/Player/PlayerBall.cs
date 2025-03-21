using UnityEngine;

namespace Player
{
    public class PlayerBall : MonoBehaviour
    {
        private bool _canShoot;

        private InputHandler _inputHandler;
        private ProjectileManager _projectileManager;
        private PlayerSizeHandler _playerSizeHandler;

        public void Inject(DependencyContainer container)
        {
            _inputHandler = container.Resolve<InputHandler>();
            _projectileManager = container.Resolve<ProjectileManager>();

            _playerSizeHandler = container.Resolve<PlayerSizeHandler>();

            _inputHandler.OnDownTouch += ResetChargeTime;
            _inputHandler.OnTouch += ChangeSize;
            _inputHandler.OnUpTouch += ReleaseShoot;
        }

        private void ResetChargeTime()
        {
            if (_canShoot) return;

            _projectileManager.GetProjectile();
            _playerSizeHandler.ResetCharging();
        }

        private void ChangeSize()
        {
            if (_canShoot) return;

            _playerSizeHandler.Shrink(Time.deltaTime);

        }

        private void ReleaseShoot()
        {
            _projectileManager.LaunchProjectile();
        }

        public void DisableShooting()
        {
            _canShoot = true;
        }

        public void Cleanup()
        {
            if (_inputHandler == null) return;

            _inputHandler.OnDownTouch -= ResetChargeTime;
            _inputHandler.OnTouch -= ChangeSize;
            _inputHandler.OnUpTouch -= ReleaseShoot;
        }
    }
}