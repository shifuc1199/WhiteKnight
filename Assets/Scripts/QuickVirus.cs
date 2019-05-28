using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class QuickVirus : Enemy {
    public float movetimer;
    public Transform[] Points;
    public float speed;
    public bool isUpdawn;
    int index;
	// Use this for initialization
	void Start () {
       
        

    }
    public void Move()
    {
        if (Vector2.Distance(transform.position, Points[index].position) <= 0.25f)
        {

            index++;
            if (!isUpdawn)
            {
                transform.localRotation = Quaternion.Euler(0, index * 180, 0);
            }else
            {
                transform.localRotation = Quaternion.Euler(0, 0, index * 90);
            }
            index %= Points.Length;
        }
        
        if(isUpdawn)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, Points[index].position.y, transform.position.z), speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(Points[index].position.x,transform.position.y, transform.position.z), speed * Time.deltaTime);

        }
        
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
    // Update is called once per frame
    void Update () {

        Move();
    }
}
