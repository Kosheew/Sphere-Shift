using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float jumpForce;

        private Rigidbody _rb;
        private bool _isGrounded;

        public void Initialize()
        {
            _rb = GetComponent<Rigidbody>();
            _isGrounded = true;
        }

        public void Move()
        {
            Vector3 velocity = _rb.velocity;
            velocity.z = speed;
            _rb.velocity = velocity;
        }

        public void Jump()
        {
            if (_isGrounded)
            {
                _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                _isGrounded = false;
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                _isGrounded = true;
            }
        }
    }
}