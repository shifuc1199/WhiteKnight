using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CopyVirus : Enemy {
    public float CopyTime;
    public float CopyRange;
    private EnemyState es;
    public bool isCopy;
  
    float tempspeed;
	// Use this for initialization
	void Start () {
        es = GetComponent<EnemyState>();
        tempspeed = GetComponent<ButterFly>().speed;
      //  InvokeRepeating("Copy", CopyTime, CopyTime);
        
    }
     


    private new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.tag == "Player")
        {

             
            
            if (collision.gameObject.GetComponent<PlayerState>().isSlim)
            {
                ItemIndexManager.总杀敌数++;
                collision.gameObject.GetComponent<PlayerCtr>().RestoreEnergy(RestoreEnergy);
                Destroy(gameObject);
            }
            else if (!collision.gameObject.GetComponent<PlayerState>().isSiphon&& !collision.gameObject.GetComponent<PlayerState>().isBurn)
            {
                collision.gameObject.GetComponent<PlayerCtr>().GetHurt();
            }
        }
    }
    IEnumerator CopyCreate()
    {
        yield return new WaitForSeconds(0.25f);
        GameObject temp = Instantiate(gameObject, transform.position, Quaternion.identity);
        temp.transform.DOMove(transform.position + new Vector3(Random.Range(-CopyRange, CopyRange), Random.Range(-CopyRange, CopyRange), Random.Range(-CopyRange, CopyRange)), 0.5f);
       
    }
    public void Copy()
    {


        if (isCopy)
        {
            if (!es.isBuff)
                return;


            GetComponent<Animator>().SetTrigger("copy");
            StartCoroutine(CopyCreate());
        }
       
    }
    // Update is called once per frame
    void Update () {
		 if(es.isdie)
        {
            GetComponent<ButterFly>().speed = 0;
            transform.DOKill();
        }
	}
}
