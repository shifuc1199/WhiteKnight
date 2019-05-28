using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PlayerSkillCtr : MonoBehaviour
{
  
    private SkillManager sm;
    private Skill NowSkill;
    private PlayerState ps;
    int SkillIndex;
    [Header("--------------技能额外需要的属性------------")]
    public float 药物壁垒持续时间;
    public float 虹吸范围;
    public float 无效化范围;
    public GameObject 远程消灭子弹;
    public GameObject 虹吸特效;
    public GameObject 药物壁垒拖尾;
    public GameObject 药物壁垒特效;
    public GameObject 无效动画;
    public Transform ShootPos;
    public  List<Collider2D> 虹吸敌人 = new List<Collider2D>();
    // Use this for initialization
    void Start () {
        ps = GetComponent<PlayerState>();
        sm = Resources.Load<SkillManager>("PlayerSkillManager");
        sm.SkillEffectSet(0, 药物壁垒);
        sm.SkillEffectSet(1, 远程消灭);
        sm.SkillEffectSet(2, 虹吸);
        sm.SkillEffectSet(3, 无效化);
        NowSkill = null;
    }
   

    //SkillOne
	 void 药物壁垒()
    {
        ps.isBurn = true;
        AudioManager._instance.AudioPlay("壁垒");
        transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, 10);
       药物壁垒特效.SetActive(true);
        药物壁垒拖尾.SetActive(true);
        StartCoroutine(药物壁垒恢复(药物壁垒持续时间));
        
    }
    IEnumerator 药物壁垒恢复(float timer)
    {
        yield return new WaitForSeconds(0.125f);
        if (transform.GetChild(0).transform.localPosition.y == 0)
        {
            transform.GetChild(0).transform.localPosition = new Vector3(0, 1, 0);
        }
        yield return new WaitForSeconds(timer);
        ps.isBurn = false;
        药物壁垒特效.SetActive(true);
         
        yield return new WaitForSeconds(0.4f);
        transform.GetChild(0).transform.localPosition = new Vector3(0, 0, 0);
        transform.GetChild(0).transform.localRotation = Quaternion.identity;
        药物壁垒拖尾.SetActive(false);
       
    }


    //SkillTwo
    void 远程消灭()
    {
        //Camera.main.DOShakePosition(0.05f, 0.01f);
        AudioManager._instance.AudioPlay("射击");
        Instantiate(远程消灭子弹, ShootPos.position,  Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y,transform.eulerAngles.z+ Random.Range(-5,5)));
    }
    void 虹吸恢复()
    {
      
        虹吸特效.SetActive(false);
        ps.isSiphon = false;
    }
    //SkillThree
    void 虹吸()
    {
        if (!ps.isSlim)
        {
            if (!ps.isSiphon)
            {
                ps.isSiphon = true;
                虹吸特效.SetActive(true);
            }

            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 虹吸范围, LayerMask.GetMask("enemy"));
           
            for (int i = 0; i < cols.Length; i++)
            {
                if (!cols[i].gameObject.GetComponent<EnemyState>().isShoot)
                {
                    if (!虹吸敌人.Contains(cols[i]))
                    {
                        虹吸敌人.Add(cols[i]);
                        cols[i].transform.DOMove(transform.position, 1f).SetEase(Ease.Linear);
                    }
                }
            }
            
        }
    }
   
    //SkillFour
    IEnumerator BuffEnable()
    {
        yield return new WaitForSeconds(0.35f);
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 无效化范围, LayerMask.GetMask("enemy"));

        for (int i = 0; i < cols.Length; i++)
        {
            cols[i].GetComponent<EnemyState>().isBuff = false;
            cols[i].GetComponent<EnemyState>().isCanEat = true;
        }
    }
    void 无效化()
    {
        
        GameObject temp=  Instantiate(无效动画, transform.position+new Vector3(0,3.5f,0),transform.rotation);
        temp.GetComponent<PlayerAnimationEvent>().Player = gameObject;

        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<PlayerState>().isSiphon = true;

        StartCoroutine(BuffEnable());
        
    }


    // Update is called once per frame
    void Update () {
        

        for (int i = 0; i < 虹吸敌人.Count; i++)
        {
          if(  虹吸敌人[i]==null)
            {
                虹吸敌人.Remove(虹吸敌人[i]);
            }
        }
        for (int i = 0; i < sm.PlayerSkills.Length; i++)
        {
            if (Input.GetKeyDown(sm.PlayerSkills[i].SkillKey)&&sm.PlayerSkills[i].isOwn&&!GetComponentInChildren<Animator>().GetBool("isPress"))
            {
                NowSkill = sm.PlayerSkills[i];
            }
        }
        
        if (NowSkill != null)
        {
            if (NowSkill.SkillName != "虹吸")
            {
                if ( NowSkill.CoolDown && GetComponent<PlayerCtr>().Energy >= NowSkill.Cost)
                {
                    GetComponent<PlayerCtr>().Energy -= NowSkill.Cost;
                    NowSkill.SkillEffect();
                    StartCoroutine(NowSkill.Cool());
                    ItemIndexManager.总技能数++;
                }
                NowSkill = null;
            }
            else
            {
                if (Input.GetKey(NowSkill.SkillKey) && NowSkill.CoolDown)
                {
                    if (GetComponent<PlayerCtr>().Energy >= NowSkill.Cost)
                    {
                        NowSkill.SkillEffect();
                        GetComponent<PlayerCtr>().Energy -= NowSkill.Cost;
                    }
                    else
                    {
                        for (int i = 0; i < 虹吸敌人.Count; i++)
                        {

                            虹吸敌人[i].transform.DOPause();

                        }
                        虹吸敌人.Clear();
                        虹吸恢复();
                        StartCoroutine(NowSkill.Cool());
                    }
                }

                if (Input.GetKeyUp(NowSkill.SkillKey) && ps.isSiphon)
                {

                    for (int i = 0; i < 虹吸敌人.Count; i++)
                    {

                        虹吸敌人[i].transform.DOPause();
                       
                    }
                    ItemIndexManager.总技能数++;
                    虹吸敌人.Clear();
                    虹吸恢复();
                    StartCoroutine(NowSkill.Cool());
                    NowSkill = null;
                }
            }
           
        }
        if (ps.cantmove)
            return;

        


    }
}
