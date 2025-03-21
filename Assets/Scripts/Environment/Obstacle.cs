using UnityEngine;

namespace Environment
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private Material normalMaterial;
        [SerializeField] private Material infectedMaterial;
        [SerializeField] private ParticleSystem explosionParticles;

        [SerializeField] private float minInfectedLifeTime;
        [SerializeField] private float maxInfectedLifeTime;

        private MeshRenderer _meshRenderer;

        public void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshRenderer.material = normalMaterial;
        }

        public void Infected()
        {
            _meshRenderer.material = infectedMaterial;
            float lifeTime = Random.Range(minInfectedLifeTime, maxInfectedLifeTime);
            Invoke(nameof(Explode), lifeTime);

        }

        private void Explode()
        {
            explosionParticles.transform.SetParent(null);
            gameObject.SetActive(false);
            explosionParticles.Play();
        }
    }
}