using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GridCell : MonoBehaviour
    {
        public Building building;
        public int cost; 
        public Sprite preview;
        public ParticleSystem particleSys;


        private void Start()
        {
            building = GetComponentInChildren<Building>(true);
            particleSys = GetComponentInChildren<ParticleSystem>();
        }

        private void Update()
        {
            if (GameManager.Instance.money >= cost && !building.isActive)
            {              
                SetParticleSystem(true);
            }
            else
            {
                SetParticleSystem(false);
            }
        }

        private void SetParticleSystem(bool activeParticle)
        {
            var particle = particleSys.emission;
            particle.enabled = activeParticle;
        }

    }
}