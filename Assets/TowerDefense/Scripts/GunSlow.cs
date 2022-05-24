using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TowerDefense.GunManager
{
    public class GunSlow : Gun
    {
        [SerializeField] ParticleSystem effectBullet;

        public float timeSlow;
        public float speedEnemyBeSlow;

        float speedEnemy;


        [SerializeField] private GameObject objDistance;
        [SerializeField] private GameObject sprGun;

        void Shoot()
        {
            speedEnemy = target.GetComponent<EnemyMove>().speedMove;
            StartCoroutine(SlowEnemy());
            //Instantiate(effectBullet, this.transform.position, Quaternion.identity, this.transform);
        }

        // process slow enemy
        IEnumerator SlowEnemy()
        {
            target.GetComponent<EnemyMove>().speedMove = speedEnemyBeSlow;
            yield return new WaitForSeconds(timeSlow);
            target.GetComponent<EnemyMove>().speedMove = speedEnemy;
        }

        IEnumerator SHOOT = null;
        IEnumerator StartShootEnemy()
        {
            while (true)
            {
                Shoot();
                yield return new WaitForSeconds(timeReloadBulletNormal);
            }
        }
        private void Start()
        {
            OnOffDistance(false);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("EnemyNormal"))
            {
                // show distance 
                OnOffDistance(true);
                if (IEturnOffDistance != null)
                    StopCoroutine(IEturnOffDistance);
            }
        }
        private void OnTriggerStay2D(Collider2D collision)
        {

            if (collision.CompareTag("EnemyNormal"))
            {
                target = collision.gameObject;
                // show distance 
                OnOffDistance(true);
                if (IEturnOffDistance != null)
                    StopCoroutine(IEturnOffDistance);
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("EnemyNormal"))
            {
                target = null;
                // show distance 

                IEturnOffDistance = OffDistance();
                StartCoroutine(IEturnOffDistance);
            }
        }
        IEnumerator IEturnOffDistance = null;
        IEnumerator OffDistance()
        {
            yield return new WaitForSeconds(1);
            OnOffDistance(false);
        }
        /// <summary>
        /// On Off Visual distance slow enemy
        /// </summary>
        /// <param name="isState">true = on, false = off</param>
        private void OnOffDistance(bool isState)
        {
            if (isState)
            {
                objDistance.SetActive(true);
                if (!sprGun.GetComponent<DOTweenAnimation>().hasOnPlay)
                    sprGun.GetComponent<DOTweenAnimation>().DOPlay();
                for (int i = 0; i < objDistance.transform.childCount; i++)
                    objDistance.transform.GetChild(i).gameObject.GetComponent<DOTweenAnimation>().DOPlay();
            }
            else
            {
                if (!sprGun.GetComponent<DOTweenAnimation>().hasOnPlay)
                    sprGun.GetComponent<DOTweenAnimation>().DOPause();
                for (int i = 0; i < objDistance.transform.childCount; i++)
                    objDistance.transform.GetChild(i).gameObject.GetComponent<DOTweenAnimation>().DOPause();
                objDistance.SetActive(false);
            }
        }
    }
}