using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleto1 : PlayerData {

    public Skeleto1() {
        NamePlayer = EnumEnemies.Skeleto1.ToString();
        IsEnemy = true;
        St = 6;
        Dx = 6;
        Iq = 2;
        Ht = 10;
        Skill skill = new Skill();
        skill.Id = 5;
        skill.Name = "Maça / Machado";
        skill.Type = "ST";
        skill.Difficult = "Média";
        skill.Nivel = 1;
        Skills.Add(skill);
    }

}
