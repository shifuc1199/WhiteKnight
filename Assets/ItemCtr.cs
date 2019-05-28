using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCtr : MonoBehaviour {
    public ItemType thisType;
    public bool isOpen;
	// Use this for initialization
	void Start () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            if (isOpen)
            {
                for (int i = 0; i < GameManager._instance.SecondTwoFountain.Length; i++)
                {
                    GameManager._instance.SecondTwoFountain[i].SetActive(true);
                }
            }
            
            UIManager._instance.ItemindexTextCtr(GetComponent<SpriteRenderer>().sprite,thisType);
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
public enum ItemType
{
    药物壁垒碎片,
    远程消灭碎片,
    虹吸碎片,
    无效化碎片,

}
