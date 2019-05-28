using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SpikedVirus : Enemy {
    public Transform[] Points;
    public float speed;
  public  int index=0;
	// Use this for initialization
	void Start () {
		
	}
    private new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.tag == "Player")
        {
           
            if (collision.gameObject.GetComponent<PlayerState>().isSlim&&!GetComponent<EnemyState>().isBuff)
            {
                collision.gameObject.GetComponent<PlayerCtr>().RestoreEnergy(RestoreEnergy);
                ItemIndexManager.总杀敌数++;
                Destroy(gameObject);
            }
            else if(!collision.gameObject.GetComponent<PlayerState>().isBurn&&  GetComponent<EnemyState>().isBuff)
            {
                
                collision.gameObject.GetComponent<PlayerCtr>().GetHurt();
            }
        }
    }
    public void Move()
    {
        if(Vector2.Distance(transform.position, Points[index].position)<=0.25f)
        {
            
            index++;
            transform.rotation = Quaternion.Euler(0, index * 180, 0);
            index %= Points.Length;
        }
        transform.position=Vector3.MoveTowards(transform.position, new Vector3(Points[index].position.x, transform.position.y, transform.position.z) , speed * Time.deltaTime);
    }
    // Update is called once per frame
    void Update () {
        Move();

    }
}
