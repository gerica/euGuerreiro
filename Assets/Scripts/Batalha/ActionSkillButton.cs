using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ActionSkillButton : MonoBehaviour {
    [SerializeField] Image imgSkill;
    [SerializeField] Sprite[] listImagens;

    public Skill Skill { get; set; }
    // Start is called before the first frame update
    void Start() {
        if (Skill.Name == EnumSkill.ARCO.GetDescription()) {
            imgSkill.sprite = listImagens[0];
        } else if (Skill.Name == EnumSkill.ESPADA_CURTA.GetDescription()) {
            imgSkill.sprite = listImagens[1];
        } else if (Skill.Name == EnumSkill.MACA_MACHADO.GetDescription()) {
            imgSkill.sprite = listImagens[2];
        } else if (Skill.Name == EnumSkill.ESPADA_LAMINA_LARGA.GetDescription()) {
            imgSkill.sprite = listImagens[3];
        } else if (Skill.Name == EnumSkill.ESPADA_DUAS_MAOS.GetDescription()) {
            imgSkill.sprite = listImagens[4];
        }

    }

    public void ActionAttack() {
        BattleManager.Instance.ActionAttack(Skill);
    }
}
