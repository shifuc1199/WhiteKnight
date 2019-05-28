using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour {
    public static UIManager _instance;

    public string EscapeKey;
    public GameObject Escape;
    public Image ShowImage;
    public Text 杀敌text;
    public Text TeachText;
    public string[] TeachContenct;
    public int TeachIndex;

    [Header("-------------道具提示------------")]
    public GameObject 道具提示;
    public GameObject TextPrefab;
    public Sprite[] SkillSprite;
   
    public Transform Itemindexpanel;

    public GameObject 图鉴;
    public GameObject 病毒;
    public GameObject 药物;
    GameObject 药物壁垒文字;
    GameObject 远程消灭文字;
    GameObject 虹吸文字;
    GameObject 无效化文字;
    SkillManager sm;
    // Use this for initialization
    void Start () {
        _instance = this;
     
        sm = Resources.Load<SkillManager>("PlayerSkillManager");
    }
    public IEnumerator 道具提示Ctr()
    {
        道具提示.transform.DOScale(1.5f, 0.5f);
        yield return new WaitForSeconds(3f);
        道具提示.transform.DOScale(0, 0.5f);
    }
    public void BackMainMenu()
    {
        Time.timeScale = 1;
        AudioManager._instance.AudioPlay("鼠标点击");
        SceneManager.LoadScene("start");
    }

    public void ContinoueGame()
    {
        AudioManager._instance.AudioPlay("鼠标点击");
        Time.timeScale = 1;
        Escape.SetActive(!Escape.activeSelf);
    }

    public void TuJianCtr()
    {
        AudioManager._instance.AudioPlay("鼠标点击");
        图鉴.SetActive(!图鉴.activeSelf);
    }
    public void 病毒Ctr()
    {
        药物.SetActive(false);
        病毒.SetActive(true);
    }
    public void 药物Ctr()
    {
        病毒.SetActive(false);
        药物.SetActive(true);
    }
    void UICtr()
    {
        if(Input.GetKeyDown(EscapeKey))
        {
           if(!Escape.activeSelf)
                Time.timeScale = 0;
           else
                Time.timeScale = 1;

            Escape.SetActive(!Escape.activeSelf);
        }
    }

   public void ItemindexTextCtr(Sprite tempsprite, ItemType it)
    {
        if (ItemIndexManager.药物壁垒碎片数 ==0&&it==ItemType.药物壁垒碎片)
        {
            ItemIndexManager.药物壁垒碎片数++;
               GameObject temp = Instantiate(TextPrefab, Itemindexpanel);
            药物壁垒文字 = temp;

            StartCoroutine(道具提示Ctr());
            道具提示.GetComponentsInChildren<Text>()[0].text = "溶酶壁垒(一)";
            道具提示.GetComponentsInChildren<Text>()[1].text = "溶酶壁垒的组成碎片之一！";
            道具提示.GetComponentInChildren<Image>().sprite = tempsprite;
            temp.GetComponent<Text>().text = "溶酶壁垒碎片数: " + ItemIndexManager.药物壁垒碎片数 + "/" + 2;
            return;
        }


        if (ItemIndexManager.远程消灭碎片数 == 0 && it == ItemType.远程消灭碎片)
        {
            GameObject temp = Instantiate(TextPrefab, Itemindexpanel);
            远程消灭文字 = temp;
            ItemIndexManager.远程消灭碎片数++;
            StartCoroutine(道具提示Ctr());
            道具提示.GetComponentsInChildren<Text>()[0].text = "干扰素射击(一)";

            道具提示.GetComponentsInChildren<Text>()[1].text = "干扰素射击药丸的组成碎片之一！";
            道具提示.GetComponentInChildren<Image>().sprite = tempsprite;

            temp.GetComponent<Text>().text = "干扰素射击碎片数: " + ItemIndexManager.远程消灭碎片数 + "/" + 2;
            return;
        }


        if (ItemIndexManager.虹吸碎片数 == 0 && it == ItemType.虹吸碎片)
        {
            ItemIndexManager.虹吸碎片数++;
            GameObject temp = Instantiate(TextPrefab, Itemindexpanel);
            虹吸文字 = temp;
            StartCoroutine(道具提示Ctr());
            道具提示.GetComponentsInChildren<Text>()[0].text = "细菌吞噬(一)";
            道具提示.GetComponentsInChildren<Text>()[1].text = "细菌吞噬的组成碎片之一！";
            道具提示.GetComponentInChildren<Image>().sprite = tempsprite;
            temp.GetComponent<Text>().text = "吞噬碎片数: " + ItemIndexManager.虹吸碎片数 + "/" + 2;
            return;
        }


        if (ItemIndexManager.无效化碎片数 == 0 && it == ItemType.无效化碎片)
        {
            ItemIndexManager.无效化碎片数++;
            GameObject temp = Instantiate(TextPrefab, Itemindexpanel);
            无效化文字 = temp;
            StartCoroutine(道具提示Ctr());
            道具提示.GetComponentsInChildren<Text>()[0].text = "病毒无效化(一)";
            道具提示.GetComponentsInChildren<Text>()[1].text = "病毒无效化的组成碎片之一！";
            道具提示.GetComponentInChildren<Image>().sprite = tempsprite;
            temp.GetComponent<Text>().text = "无效化碎片数: " + ItemIndexManager.无效化碎片数 + "/" + 2;
            return;
        }











         if (ItemIndexManager.药物壁垒碎片数==1 && it == ItemType.药物壁垒碎片)
        {
            StartCoroutine(道具提示Ctr());
            sm.PlayerSkills[0].isOwn = true;
            道具提示.GetComponentsInChildren<Text>()[0].text = "合成成功！\n获得新能力：药物壁垒";
            道具提示.GetComponentsInChildren<Text>()[1].text = "按K键释放:在玩家周围生成一团药物火焰，碰到的病毒都会瞬间爆炸！";
            道具提示.GetComponentInChildren<Image>().sprite = SkillSprite[0];
            Destroy(药物壁垒文字);
            ItemIndexManager.药物壁垒碎片数 = 0;
        }
          if (ItemIndexManager.远程消灭碎片数 == 1 && it == ItemType.远程消灭碎片)
        {
            StartCoroutine(道具提示Ctr());
            sm.PlayerSkills[1].isOwn = true;
            道具提示.GetComponentsInChildren<Text>()[0].text = "合成成功！\n获得新能力：抗生素射击";
            道具提示.GetComponentsInChildren<Text>()[1].text = "J键释放:可射出抗生素消灭病菌！";
            道具提示.GetComponentInChildren<Image>().sprite = SkillSprite[1];
            Destroy(远程消灭文字);
            ItemIndexManager.远程消灭碎片数 = 0;
        }
          if (ItemIndexManager.虹吸碎片数 == 1 && it == ItemType.虹吸碎片)
        {
            StartCoroutine(道具提示Ctr());
            sm.PlayerSkills[2].isOwn = true;
            道具提示.GetComponentsInChildren<Text>()[0].text = "合成成功！\n获得新能力:病毒吞噬";
            道具提示.GetComponentsInChildren<Text>()[1].text = "按住L键释放:将病毒吸到身边，低等级直接吞噬！";
            道具提示.GetComponentInChildren<Image>().sprite = SkillSprite[2];
            Destroy(虹吸文字);
            ItemIndexManager.虹吸碎片数 = 0;
        }
          if (ItemIndexManager.无效化碎片数 == 1 && it == ItemType.无效化碎片)
        {
            StartCoroutine(道具提示Ctr());
            sm.PlayerSkills[3].isOwn = true;
            道具提示.GetComponentsInChildren<Text>()[0].text = "合成成功！\n获得新能力:病毒无效化";
            道具提示.GetComponentsInChildren<Text>()[1].text = "O键释放:用分子能力改变病毒结构，使其失去活性";
            道具提示.GetComponentInChildren<Image>().sprite = SkillSprite[3];
            Destroy(无效化文字);
            ItemIndexManager.无效化碎片数 = 0;
        }
    }

	// Update is called once per frame
	void Update () {
        杀敌text.text = "消灭病毒: " + ItemIndexManager.总杀敌数;
        if (TeachIndex <=TeachContenct.Length - 1)
        {
            TeachText.text = TeachContenct[TeachIndex];
            TeachText.text = TeachText.text.Replace("\\n", "\n");
        }
        else
        {
            TeachText.text = "";
        }
        UICtr();
        
    }
}
