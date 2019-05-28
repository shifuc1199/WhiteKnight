using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Skill
{
    public Action SkillEffect;
    public float Cost;
    public string SkillKey;
    public string SkillName;
    public float CoolTime;
    public bool isOwn;
    public bool CoolDown;
    float timer;
    public  IEnumerator Cool()
    {
        CoolDown = false;
        yield return new WaitForSeconds(CoolTime);
        CoolDown = true;
    }
    
     
}
