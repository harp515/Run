using System;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class Player : MonoBehaviour
    {
        [SerializeField] float jumpForce = 600.0f, doublejumpForce = 600.0f;
        private Rigidbody2D _rd;
        public Slider hpSlider;

        private bool _doubleJumpState = true;
        private bool _isground = true;
        
        private bool getDamage = true;
        private float currentTime = 0;

        public float noDamage = 2.0f;
        public float maxhp = 300.0f;
        public float hp = 300.0f;
        public float Damage = 30;
        public float SpinSpeed = 9;

        private void Start()
        {
            _rd = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            hpSlider.value = (float)hp / (float)maxhp;
            Jump();
            GetDamage();
            Spin();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        void Jump()
        {
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
            _isground = false;
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Wall") && getDamage)
            {
                hp -= Damage;
                if(currentTime == 0)
                    getDamage = false;
            }
            if (collision.gameObject.CompareTag("Floor")) {
                Debug.Log("부딪힘");
                _isground = true;
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

        void Spin()
        {
            transform.Rotate(Vector3.back * Time.deltaTime * SpinSpeed);
        }
    }
}
