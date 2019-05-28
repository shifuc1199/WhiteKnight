using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainCtr : MonoBehaviour {
    public float FountainSpeed;
    public GameObject FountainEffect;
	// Use this for initialization
	void Start () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            collision.GetComponent<Rigidbody2D>().gravityScale = collision.GetComponent<PlayerCtr>().StartGravity;
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
          collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up * FountainSpeed,ForceMode2D.Impulse);
        }
        if (collision.gameObject.tag == "stone")
        {
            FountainEffect.SetActive(false);
            gameObject.SetActive(false);
        }
        }
    // Update is called once per frame
    void Update () {
		
	}
}
