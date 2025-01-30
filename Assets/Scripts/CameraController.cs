using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraController : MonoBehaviour
    {
        public Vector3 target; 
        public float rotationSpeed = 100; 
        public float minAngle = 10; 
        public float maxAngle = 80; 

        private float _currentDistance = 7; 
        private Vector3 _currentRotation;

        private void Start()
        {
            _currentRotation = transform.eulerAngles;
        }

        private void Update()
        {
            HandleRotation();   
            UpdateCameraPosition(); 
        }

        private void HandleRotation()
        {
            if (Input.GetMouseButton(1)) 
            {
                float mouseX = Input.GetAxis("Mouse X");

                _currentRotation.y += mouseX * rotationSpeed * Time.deltaTime; 

                _currentRotation.x = Mathf.Clamp(_currentRotation.x, minAngle, maxAngle);
            }
        }

        private void UpdateCameraPosition()
        {
            Quaternion rotation = Quaternion.Euler(_currentRotation.x, _currentRotation.y, 0);
            Vector3 direction = rotation * Vector3.forward;

            transform.position = target - direction * _currentDistance;

            transform.LookAt(target);
        }
    }
}