using System;
using UnityEngine;

namespace Game.Entity.Movement
{
    public sealed class CameraFollow : MonoBehaviour 
    {
        [SerializeField] private Transform target;
        [SerializeField] private float smoothSpeed;
        
        private Vector3 _offset;

        private void Awake()
        {
            _offset = transform.position - target.position;
        }

        public void Update()
        {
            var desiredPosition = target.position + _offset;
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}