using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Script
{
    public class Player : MonoBehaviour
    {
        [SerializeField] float jumpForce = 50.0f;
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

        public int score = 0;
        public int setScore = 20;

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

            if (_isground && Input.GetButton("Jump"))
                JumpAddForce();
            else if (_doubleJumpState && Input.GetButtonDown("Jump"))
            {   
                _doubleJumpState = false;
                JumpAddForce();
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        void JumpAddForce()
        {
            _rd.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
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
                _isground = true;
            }
            if (collision.gameObject.CompareTag("Coin"))
            {
                score += Random.Range(setScore/4,setScore);
            }
        }

        void GetDamage()
        {
            if (!getDamage)
                currentTime += Time.deltaTime;
            if (currentTime > noDamage) {
                    getDamage = true;
                    currentTime = 0;
            }
        }

        void Spin()
        {
            transform.Rotate(Vector3.back * (Time.deltaTime * SpinSpeed));
        }
    }
}
