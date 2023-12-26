using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Script
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float jumpForce = 12.0f;
        private Rigidbody2D _rd;
        public Slider hpSlider;

        private bool _doubleJumpState = true;
        private bool _isground = true;
        private bool _getDamage = true;
        
        private float _currentTime = 0;

        private readonly float _noDamage = 2.0f;
        private const float Maxhp = 300.0f;
        public float hp = 300.0f;
        private float _damage = 30;
        private readonly float _spinSpeed = 270;
        private float addWallSpeed;

        private void Awake()
        {
            _rd = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            addWallSpeed += Time.deltaTime * 0.05f;
            if (hp > Maxhp)
                hp = Maxhp;
            hpSlider.value = (float)hp / (float)Maxhp;
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
                JumpAddForce(jumpForce);
            else if (_doubleJumpState && Input.GetButtonDown("Jump"))
            {   
                _doubleJumpState = false;
                JumpAddForce(jumpForce * 0.75f);
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (_isground && touch.phase == TouchPhase.Began)
                    JumpAddForce(jumpForce);
                else if (_doubleJumpState && touch.phase == TouchPhase.Began)
                {   
                    _doubleJumpState = false;
                    JumpAddForce(jumpForce * 0.75f);
                }
            }
            
        }

        // ReSharper disable Unity.PerformanceAnalysis
        void JumpAddForce(float jumpPower)
        {
            _rd.AddForce(new Vector2(0f, jumpPower), ForceMode2D.Impulse);
            _isground = false;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Wall") && _getDamage)
            {
                hp -= _damage;
                if (_currentTime == 0)
                    _getDamage = false;
            }

            if (collision.gameObject.CompareTag("Moon") && _getDamage)
            {
                hp -= _damage * 2.5f;
                if (_currentTime == 0)
                    _getDamage = false;
            }

            if (collision.gameObject.CompareTag("Floor"))
            {
                _isground = true;
            }

            if (collision.gameObject.CompareTag("Heal"))
            {
                hp += 15;
                Destroy(collision.gameObject);
            }
        }

        void GetDamage()
        {
            if (!_getDamage)
                _currentTime += Time.deltaTime;
            if (_currentTime > _noDamage) {
                    _getDamage = true;
                    _currentTime = 0;
            }
        }

        void Spin()
        {
            GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
            transform.Rotate(Vector3.back * (Time.deltaTime * (_spinSpeed + addWallSpeed * 2)));
        }
    }
}