using System;
using Environment;
using UnityEngine;

namespace Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private float timeResetPool;
        [SerializeField] private float radiusInfected;
        [SerializeField] private float coefficientSize;
        private Rigidbody _rb;

        private float _radius;

        public event Action<Projectile> OnDestroyed;

        public void SetSize(float size)
        {
            transform.localScale = Vector3.one * (size * coefficientSize);
            _radius = size * coefficientSize * radiusInfected;
        }

        public void Launch()
        {
            if (_rb == null)
                _rb = GetComponent<Rigidbody>();

            _rb.AddForce(transform.forward * speed, ForceMode.Impulse);
            Invoke(nameof(Destroyed), timeResetPool);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Obstacle obstacle))
            {
                CancelInvoke(nameof(Destroyed));
                InfectArea();
                Destroyed();
            }
        }

        private void InfectArea()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _radius);
            foreach (var collider in hitColliders)
            {
                if (collider.TryGetComponent(out Obstacle obstacle))
                {
                    obstacle.Infected();
                }
            }
        }

        public void Destroyed()
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            OnDestroyed?.Invoke(this);
        }
    }
}