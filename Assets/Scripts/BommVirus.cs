using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BommVirus : Enemy {
    public GameObject bullet;
    public float Shoottimer;
    public Transform ShootPos;
	// Use this for initialization
	void Start () {
        InvokeRepeating("Shoot", Shoottimer, Shoottimer);
	}
    private new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.tag == "Player")
        {
             
            if (collision.gameObject.GetComponent<PlayerState>().isSlim && !GetComponent<EnemyState>().isBuff)
            {
                ItemIndexManager.总杀敌数++;
                Destroy(gameObject);
            }
           
        }
    }
    IEnumerator CreatBullet()
    {
        yield return new WaitForSeconds(0.28f);
        Instantiate(bullet, ShootPos.position, transform.rotation);
    }
    public void Shoot()
    {
        if (GetComponent<EnemyState>().isBuff)
        {
            StartCoroutine(CreatBullet());
            GetComponent<Animator>().SetTrigger("shoot");
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
