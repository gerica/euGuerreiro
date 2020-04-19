using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {
    [SerializeField] GameObject battleScene;
    [SerializeField] GameObject targetsAttack;

    // Start is called before the first frame update
    void Start() {
        targetsAttack.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            BattleStart();
        }
        if (Input.GetKeyDown(KeyCode.Y)) {
            BattleEnd();
        }
    }

    private void BattleStart() {
        Camera.main.orthographicSize = 6.0f;
        transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
        GameManager.Instance.BattleActive = true;
        battleScene.SetActive(true);
    }

    private void BattleEnd() {
        Camera.main.orthographicSize = 12.0f;
        GameManager.Instance.BattleActive = false;
        battleScene.SetActive(false);
    }

    public void ActionAttack() {
        targetsAttack.SetActive(true);
    }
}
