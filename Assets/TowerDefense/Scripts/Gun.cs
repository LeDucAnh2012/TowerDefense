using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NameGun
{
    normal,
    Name1
}
public enum TargetGun
{
    Furthest,
    Nearest,
    Weakest,
    Strongest,
    TheFastest
}
namespace TowerDefense.GunManager
{
    public class Gun : MonoBehaviour
    {
        public GameObject spriteGun;

        public NameGun nameGun;
        public TargetGun targetGun;

        public int levelGun = 1;
        public int priceGun = 20;

        public bool isShoot = false;
        public float distanceAttack = 2f;
        public float speedAttack = 2f;

        public float timeReloadBulletNormal = 2f;

        public bool autoActivePower = false;

        public float timeHasBulletPower = 2f;
        public float timeReloadBulletPower = 2f;

        public GameObject target = null;

        public int damageGun = 20;

        private void OnValidate()
        {
            GetComponent<CircleCollider2D>().radius = distanceAttack;
        }
        protected virtual void Attack()
        {

        }
        protected virtual void SetDirection()
        {
            if (target == null)
                return;
            Vector3 dir = target.transform.position - this.transform.position;
            dir = target.transform.InverseTransformDirection(dir);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            spriteGun.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}