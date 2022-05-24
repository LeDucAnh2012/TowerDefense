using System.Collections;
using System.Collections.Generic;
using TowerDefense.GunManager;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] GameObject spriteObject;

    public float speedMove = 1.5f;

    private List<GameObject> listPointDirection = new List<GameObject>();

    int countPoint = 0;
    float speedCurrent;

    float posX = 0;
    float posY = 0;
    // Start is called before the first frame update
    void Start()
    {
        speedCurrent = speedMove;
        listPointDirection = GamePlayController.instance.listPointDirection;
    }

    // Update is called once per frame
    void Update()
    {

        if (this.transform.position == listPointDirection[listPointDirection.Count - 1].transform.position)
            Destroy(this.gameObject);

        if (countPoint >= listPointDirection.Count)
            return;

        if (this.transform.position == listPointDirection[countPoint].transform.position)
            countPoint++;

        if (countPoint >= listPointDirection.Count)
            return;

        Vector3 dir = listPointDirection[countPoint].transform.position - transform.position;
        dir = listPointDirection[countPoint].transform.InverseTransformDirection(dir);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        spriteObject.transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        transform.position = Vector3.MoveTowards(this.transform.position, listPointDirection[countPoint].transform.position, Time.deltaTime * speedMove);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GunSlow"))
        {
            speedMove = collision.gameObject.GetComponent<GunSlow>().speedEnemyBeSlow;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("GunSlow"))
        {
            speedMove = speedCurrent;
        }

    }
}
