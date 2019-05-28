using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ButterFly : MonoBehaviour {

    public float speed = 3.0f;  //移动速度
    public float obstacleRange ;  //反应距离
    
    
    void Start()
    {
       gameObject.SetActive(true);
        
        
    }
  
     
    void Update()
    {
        Move();
    }

    void Move() //移动
    {

         
        if (gameObject.activeSelf)
        {
            transform.Translate(transform.right * speed * Time.deltaTime,Space.World);

        }
        else return;

        //利用射线检测碰撞，碰撞后变向
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right,10000,LayerMask.GetMask("ground"));
        
        
        if (hit.collider != null&&(hit.collider.gameObject.layer==LayerMask.NameToLayer("ground")))
        {
 
            if (hit.distance < obstacleRange)
            {
                float angle = Random.Range(-90, -180);
                transform.Rotate(0, 0, transform.eulerAngles.z+ angle+23);
            }
        }
    }
  
   

}
