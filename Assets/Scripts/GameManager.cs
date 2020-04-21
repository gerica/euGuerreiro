using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private bool gameMenuOpen;
    public static GameManager Instance { get; set; }
    public bool BattleActive { get; set; }
    public bool GameMenuOpen { get => gameMenuOpen; set => gameMenuOpen = value; }

    // Start is called before the first frame update
    void Start() {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update() {
        if (BattleActive || GameMenuOpen) {
            PlayerController.Instance.CanMove = false;
        } else {
            PlayerController.Instance.CanMove = true;
        }

    }
}
