using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
    public float speed;
	// Use this for initialization
	void Start () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player"&&!collision.gameObject.GetComponent<PlayerState>().isBurn)
        {
            collision.gameObject.GetComponent<PlayerCtr>().GetHurt();
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            Destroy(gameObject);
        }
    
       
    }
    // Update is called once per frame
    void Update () {
        transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
    }
}
