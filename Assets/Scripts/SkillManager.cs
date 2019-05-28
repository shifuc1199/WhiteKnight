using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
[CreateAssetMenu(menuName = "SkillManager")]
public class SkillManager : ScriptableObject {

    public Skill[] PlayerSkills;
    public void SkillEffectSet(int index,Action ac)
    {
        PlayerSkills[index].isOwn = false;
        PlayerSkills[index].CoolDown = true;
        PlayerSkills[index].SkillEffect = ac;
    }
}
