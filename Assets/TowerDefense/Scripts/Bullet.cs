using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speedBullet = 2f;
    [SerializeField] GameObject spriteBullet;

    bool isMove = false;

    GameObject target = null;
    int damage;
    //=================================

    public void SetTarget(GameObject _target, int _damage,float _speedBullet)
    {
        isMove = true;
        target = _target;
        damage = _damage;
        speedBullet = _speedBullet;
    }
    void MoveToTaget()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }
        if (transform.position == target.transform.position)
        {
            isMove = false;

            Destroy(this.gameObject);
            return;
        }
        transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, Time.deltaTime * speedBullet);
    }
    //===================================================

    void SetDirection()
    {
        if (target == null)
            return;
        Vector3 dir = target.transform.position - transform.position;
        dir = target.transform.InverseTransformDirection(dir);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        spriteBullet.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    private void Update()
    {
        if (!isMove)
            return;
        MoveToTaget();
        SetDirection();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyNormal") && target == collision.gameObject)
        {
            target.GetComponent<EnemyHealth>().enemyHealth -= damage;
            target.GetComponent<EnemyHealth>().ReduceHealth(damage);
            if (target.GetComponent<EnemyHealth>().enemyHealth <= 0)
                Destroy(target);
            Destroy(this.gameObject);
        }
    }
}
