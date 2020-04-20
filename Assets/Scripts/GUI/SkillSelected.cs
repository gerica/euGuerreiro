using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelected : MonoBehaviour {
    [SerializeField] Text txtSkill;
    private string txtSkillValue;
    [SerializeField] Text txtNivel;
    private string txtNivelValue;

    public string TxtSkillValue { get => txtSkillValue; set => txtSkillValue = value; }
    public string TxtNivelValue { get => txtNivelValue; set => txtNivelValue = value; }

    void Start() {
        txtSkill.text = TxtSkillValue;
        txtNivel.text = TxtNivelValue;
    }

}
