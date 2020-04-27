using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePlayer : MonoBehaviour {
    [SerializeField] private GameObject[] armsPrefab;
    public PlayerData Player { get; set; }
    public GameObject[] ArmsPrefab { get => armsPrefab; set => armsPrefab = value; }

    public GameObject GetCurrentArmPrefab(Skill skillSelected) {

        //Load a text file (Assets/Resources/Text/textFile01.txt)
        //var textFile = Resources.Load<TextAsset>("Text/textFile01");
        return Instantiate(Resources.Load("Prefabs/Batlle/Arms/arcoFecha")) as GameObject;
        //if (skillSelected.Name == EnumSkill.ARCO.GetDescription()) {
        //    return armsPrefab[0];
        //} else if (skillSelected.Name == EnumSkill.ESPADA_CURTA.GetDescription()) {
        //    return armsPrefab[1];
        //} else if (skillSelected.Name == EnumSkill.MACA_MACHADO.GetDescription()) {
        //    return armsPrefab[2];
        //} else if (skillSelected.Name == EnumSkill.ESPADA_LAMINA_LARGA.GetDescription()) {
        //    return armsPrefab[3];
        //} else if (skillSelected.Name == EnumSkill.ESPADA_DUAS_MAOS.GetDescription()) {
        //    return armsPrefab[4];
        //}

        //return null;
    }

    public Sprite GetSprite() {
        return GetComponentInParent<Image>().sprite;
    }

}
