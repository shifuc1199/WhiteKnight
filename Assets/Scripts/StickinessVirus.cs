using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StickinessVirus : Enemy {
     
	// Use this for initialization
	void Start () {
		
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
            }else if(!collision.gameObject.GetComponent<PlayerState>().isSiphon)
            {
                collision.gameObject.GetComponent<PlayerCtr>().GetHurt();
            }
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
