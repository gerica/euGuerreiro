using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTarget : MonoBehaviour {
    [SerializeField] Text nameTarget;
    public BattlePlayer Target { get; set; }

    // Start is called before the first frame update
    void Start() {
        if (nameTarget && Target) {
            nameTarget.text = Target.Player.NamePlayer;
        }
    }

    public void OnSelectTarget() {
        Debug.Log(Target);
        BattleManager.Instance.SelectTarget(Target);
    }
}
