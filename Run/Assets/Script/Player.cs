using System;
using UnityEngine;

namespace Script
{
    public class Player : MonoBehaviour
    {
        [SerializeField] float jumpForce = 600.0f, doublejumpForce = 700.0f;
        private Rigidbody2D _rd;

        private bool _doubleJumpState = false;
        private bool _isground = false;

        private void Start()
        {
            _rd = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Jump();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        void Jump()
        {
            if (_rd.velocity.y == 0)
            {
                _isground = true;
            }
            else
                _isground = false;

            if (_isground)
            {
                _doubleJumpState = true;
            }

            if (_isground && Input.GetButtonDown("Jump"))
                JumpAddForce();
            else if (_doubleJumpState && Input.GetButtonDown("Jump"))
            {
                _doubleJumpState = false;
                _rd.AddForce(Vector2.up * doublejumpForce);
                Debug.Log("더블 점프");
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        void JumpAddForce()
        {
            Debug.Log("점프");
            _rd.AddForce(Vector2.up * jumpForce);
        }
    }
}
