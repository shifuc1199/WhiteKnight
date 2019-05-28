using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copyCtr : MonoBehaviour {


    public List<GameObject> copyList;
    public List<GameObject> copyNowList;
    public int copyNumber = 16;
    public GameObject 复制病毒;
    private int countCopy = 0;
    private bool isAlive;
    private int countNow;
    void Awake()
    {

        creatCopy();
    }

    void Start()
    {
        copyList[0].transform.position = gameObject.transform.position;
        copyList[0].SetActive(true);
        copyNowList.Add(copyList[0]);
        InvokeRepeating("copyAct", 2f, 3.5f);
    }
    void creatCopy()
    {
        copyList = new List<GameObject>();         //初始化链表
        for (int i = 0; i < copyNumber; ++i)
        {
            GameObject copyObj = Instantiate(复制病毒);    //创建对象
            copyObj.SetActive(false);                       //设置无效
            copyList.Add(copyObj);                     //添加到链表（对象池）中
        }
    }

void copyAct() //复制操作，出链表,初始位置和上一个相同
    {
        int orederCopy = copyList.Count - copyNowList.Count;
        if (orederCopy >0)
        {
            countNow = copyNowList.Count;
            for (int i=0;i< countNow; i++)
        {


                if (copyNowList[i])
                {
                    if (copyNowList[i].GetComponent<EnemyState>().isBuff)
                    {
                        copyList[i + countNow].transform.position = copyNowList[i].transform.position;
                        copyList[i + countNow].SetActive(true);


                        if (copyNowList.Count < copyNumber) copyNowList.Add(copyList[i + countNow]);
                        orederCopy = copyList.Count - copyNowList.Count;
                        if (orederCopy <= 0) break;

                    }
                }
            }
        }
    }


}

