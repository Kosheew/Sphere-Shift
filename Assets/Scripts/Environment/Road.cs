using Player;
using UnityEngine;

namespace Environment
{
    public class Road : MonoBehaviour
    {
        private PlayerSizeHandler _playerSizeHandler;

        public void Inject(DependencyContainer container)
        {
            _playerSizeHandler = container.Resolve<PlayerSizeHandler>();

            _playerSizeHandler.OnSizeChange += UpdateWidth;
        }

        private void UpdateWidth(float width)
        {
            transform.localScale = new Vector3(width, transform.localScale.y, transform.localScale.z);
        }
    }
}