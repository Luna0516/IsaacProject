using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_TearDrop : MonoBehaviour
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 10f;
        public float lifeTime = 2f;
        public float distanceThreshold = 10f;
        public float cooldown = 1f;

        private float currentCooldown = 0f;
        private bool isFired = false;
        private Vector3 initialPosition;

        private void Start()
        {
            initialPosition = transform.position;
        }

        private void Update()
        {
            if (!isFired)
                return;

            Move();

            if (HasExceededDistanceThreshold())
            {
                DestroyBullet();
                return;
            }

            if (HasExpiredLifeTime())
            {
                DestroyBullet();
            }
        }

        private void Move()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private bool HasExceededDistanceThreshold()
        {
            float distance = Vector3.Distance(transform.position, initialPosition);
            return distance >= distanceThreshold;
        }

        private bool HasExpiredLifeTime()
        {
            return Time.time >= (currentCooldown + lifeTime);
        }

        private void DestroyBullet()
        {
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                DestroyBullet();
            }
            else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))
            {
                DestroyBullet();
            }
        }

        public void Fire()
        {
            if (currentCooldown <= 0f)
            {
                isFired = true;
                currentCooldown = cooldown;
            }
        }

        private void FixedUpdate()
        {
            if (currentCooldown > 0f)
            {
                currentCooldown -= Time.fixedDeltaTime;
            }
        }
    }

}
