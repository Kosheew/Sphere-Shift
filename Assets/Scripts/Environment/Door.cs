using Player;
using UnityEngine;

namespace Environment
{
    public class Door : MonoBehaviour
    {
        private VictoryDefeatManager _victoryDefeatManager;

        public void Inject(DependencyContainer container)
        {
            _victoryDefeatManager = container.Resolve<VictoryDefeatManager>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out PlayerBall playerBall))
            {
                _victoryDefeatManager.OnVictory?.Invoke();
            }
        }
    }
}