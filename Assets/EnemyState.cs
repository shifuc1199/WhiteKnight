using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyState : MonoBehaviour {
    public bool isBuff;
    public bool isCanEat;
    public GameObject Debuff;
    GameObject player;
    public int health;
    public bool isShoot;
    public bool isdie;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Die()
    {
        AudioManager._instance.AudioPlay("怪物死亡");
        player.GetComponent<PlayerCtr>().RestoreEnergy(GetComponent<Enemy>().RestoreEnergy);
        GetComponent<Animator>().SetTrigger("die");
        ItemIndexManager.总杀敌数++;
        GetComponent<PolygonCollider2D>().enabled = false;
        Destroy(gameObject, 0.5f);
    }
   
    IEnumerator ColorBack()
    {
        yield return new WaitForSeconds(0.2f);
        transform.GetComponentInChildren<SpriteRenderer>().DOColor(Color.white, 0.2f);
       
    }
    public void GetHurt()
    {
        health -= 1;
        transform.GetComponentInChildren<SpriteRenderer>().DOColor(Color.red, 0.2f);
        StartCoroutine(ColorBack());
        if (health<=0&&!isdie)
        {
            isdie = true;
            Die();
        }
    }
	// Update is called once per frame
	void Update () {
		if(Debuff.GetComponent<SpriteRenderer>().color.a==0&& !isBuff)
        {
            Debuff.GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
        }
	}
}
