using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.GunManager
{
    public class GunNormal : Gun
    {
        [SerializeField] Bullet bullet;
        private void Update()
        {
            base.SetDirection();
        }

        void CheckTheEnemyHasEnteredTheRange(GameObject _target)
        {
            if (isShoot)
                return;
            //if (_target.transform.position.y < this.transform.position.y + distanceAttack &&
            //    _target.transform.position.y > this.transform.position.y - distanceAttack &&
            //    _target.transform.position.x > this.transform.position.x - distanceAttack &&
            //    _target.transform.position.x < this.transform.position.x + distanceAttack
            //    )
            if (Vector3.Distance(_target.transform.position, this.transform.position) - 0.5f <= distanceAttack)
                isShoot = true;
        }
        //=================================================================
        void Shoot()
        {
            if (!isShoot)
                return;
            if (target == null)
            {
                isShoot = false;
                return;
            }

            Bullet _bullet = Instantiate(bullet, this.transform.position, Quaternion.identity, this.transform);
            target.transform.parent = target.transform;
            _bullet.SetTarget(target, damageGun, speedAttack);
        }
        IEnumerator SHOOT = null;
        IEnumerator StartShootEnemy()
        {
            while (true)
            {
                yield return new WaitForSeconds(timeReloadBulletNormal);
                Shoot();
            }
        }
        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    if (target == null)
        //        if (collision.CompareTag("EnemyNormal"))
        //        {
        //            target = collision.gameObject;
        //            isShoot = true;
        //        }
        //}
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (target == null & !isShoot)
                if (collision.CompareTag("EnemyNormal"))
                {
                    isShoot = true;
                    target = collision.gameObject;
                    SHOOT = StartShootEnemy();
                    StartCoroutine(SHOOT);
                }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (target != null)
            {
                if (collision.CompareTag("EnemyNormal") && collision.gameObject == target)
                {
                    isShoot = false;
                    target = null;
                    if (SHOOT != null)
                        StopCoroutine(SHOOT);
                }
            }
        }
    }
}