using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealth;
    public List<GameObject> listBulletBeAttack;

    public Image imgHealth;


    int maxHealth;
    private void Start()
    {
        maxHealth = enemyHealth;
    }
    public void ReduceHealth(int _damage)
    {
        float _valueReduce = (float)_damage / maxHealth;
        imgHealth.fillAmount -= _valueReduce;
    }
    private void OnDestroy()
    {
        int i = 0;
        while (i < listBulletBeAttack.Count)
        {
            if(listBulletBeAttack[i] == null)
            {
                listBulletBeAttack.RemoveAt(i);
            }
            else
            {
                Destroy(listBulletBeAttack[i]);
                listBulletBeAttack.RemoveAt(i);
            }
            i++;
        }
    }
}
