using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayer : MonoBehaviour {
    [SerializeField] private GameObject[] armsPrefab;
    public PlayerData Player { get; set; }
    public GameObject[] ArmsPrefab { get => armsPrefab; set => armsPrefab = value; }

    public GameObject GetCurrentArmPrefab(Skill skillSelected) {
        if (skillSelected.Name == EnumSkill.ARCO.GetDescription()) {
            return armsPrefab[0];
        } else if (skillSelected.Name == EnumSkill.ESPADA_CURTA.GetDescription()) {
            return armsPrefab[1];
        } else if (skillSelected.Name == EnumSkill.MACA_MACHADO.GetDescription()) {
            return armsPrefab[1];
        } else if (skillSelected.Name == EnumSkill.ESPADA_LAMINA_LARGA.GetDescription()) {
            return armsPrefab[1];
        } else if (skillSelected.Name == EnumSkill.ESPADA_DUAS_MAOS.GetDescription()) {
            return armsPrefab[1];
        }

        return null;

    }

}
