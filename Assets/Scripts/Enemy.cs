using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Enemy : MonoBehaviour {

    public float RestoreEnergy;
    
    public void Die()
    {
        GetComponent<EnemyState>().isdie = true;
        GetComponent<Animator>().SetTrigger("die");
        GetComponent<PolygonCollider2D>().enabled = false;
        AudioManager._instance.AudioPlay("怪物死亡");
        Destroy(gameObject,0.5f);
        ItemIndexManager.总杀敌数++;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerState>().isBurn)
            {
               
                collision.gameObject.GetComponent<PlayerCtr>().RestoreEnergy(RestoreEnergy);
                Die();
              
                return;
            }

            if (collision.gameObject.GetComponent<PlayerState>().isSiphon)
            {
               
                if (GetComponent<EnemyState>().isCanEat)
                {

                    collision.gameObject.GetComponent<PlayerCtr>().RestoreEnergy(RestoreEnergy);
                    AudioManager._instance.AudioPlay("怪物死亡");
                    Destroy(gameObject);
                    ItemIndexManager.总杀敌数++;
                    return;
                }
                else
                {
                    if (!collision.gameObject.GetComponent<PlayerState>().isHurt)
                    {
                        collision.gameObject.GetComponent<PlayerCtr>().GetHurt();
                        transform.DOKill();
                    }
                }
            }

        }
    }


}
