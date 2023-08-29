using System;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class Player : MonoBehaviour
    {
        [SerializeField] float jumpForce, doublejumpForce;
        private Rigidbody2D _rd;
        public Slider hpSlider;

        private bool _doubleJumpState = false;
        private bool _isground = false;
        
        private bool getDamage = true;
        private float currentTime = 0;

        public float noDamage = 2.0f;
        public float maxhp = 300.0f;
        public float hp = 300.0f;
        public float Damage = 30;

        private void Start()
        {
            _rd = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            hpSlider.value = (float)hp / (float)maxhp;
            Jump();
            GetDamage();
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
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Wall") && getDamage)
            {
                hp -= Damage;
                getDamage = false;
            }
        }

        void GetDamage()
        {
            if (!getDamage)
                currentTime += Time.deltaTime;
                if (currentTime > noDamage)
                {
                    getDamage = true;
                    currentTime = 0;
                }
        }
    }
}
