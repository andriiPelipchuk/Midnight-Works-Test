using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Door : MonoBehaviour
    {
        public Vector3 direction;
        public float distance;
        public LayerMask playerLayer;
        public string clip;

        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            Ray ray = new Ray(transform.position, direction);
            Debug.DrawRay(transform.position, direction, Color.red);

            if (Physics.Raycast(ray, out RaycastHit hit, distance, playerLayer))
            {
                animator.Play(clip);
            }
        }
    }
}