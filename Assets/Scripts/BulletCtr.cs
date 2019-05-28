using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BulletCtr : MonoBehaviour {
    public float _speed;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, 0.5f);
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="line")
        {
            collision.transform.GetChild(0).GetComponent<Rigidbody2D>().gravityScale = 3;
             
        }
        if (collision.gameObject.tag != "Player"&& collision.gameObject.tag!="stone" && collision.gameObject.tag != "health")
        {
            Camera.main.transform.parent.DOShakePosition(0.1f, 0.2f);
            Destroy(gameObject);
        }
        if (collision.gameObject.layer ==LayerMask.NameToLayer("enemy"))
        {
            collision.gameObject.GetComponent<EnemyState>().GetHurt();
        }
    }
    // Update is called once per frame
    void Update () {
        transform.Translate(transform.right * _speed * Time.deltaTime,Space.World);
	}
}
