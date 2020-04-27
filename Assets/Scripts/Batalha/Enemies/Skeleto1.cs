using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleto1 : PlayerData {

    public Skeleto1() {
        NamePlayer = EnumEnemies.Skeleto1.ToString();
        IsEnemy = true;
        St = 11;
        Dx = 6;
        Iq = 2;
        Ht = 10;
        HtPlayer = 10;
        Skill skill = new Skill();
        skill.Id = 5;
        skill.Name = ShortSword.skillToUse;
        skill.Type = "ST";
        skill.Difficult = "Média";
        skill.Nivel = 2;
        Skills.Add(skill);
    }

}
