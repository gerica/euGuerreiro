//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour {
    [SerializeField] GameObject battleScene;
    [SerializeField] GameObject targetPanel;
    [SerializeField] GameObject dicePanel;
    [SerializeField] GameObject playDice;
    [SerializeField] Text[] dices;
    [SerializeField] float speedDice = 0.1f;

    private int typeDice = 7;

    public int TypeDice { get => typeDice; set => typeDice = value; }

    // Start is called before the first frame update
    void Start() {
        targetPanel.SetActive(false);
        dicePanel.SetActive(false);
        playDice.SetActive(false);
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
        targetPanel.SetActive(false);
        playDice.SetActive(false);
    }

    public void ActionAttack() {
        targetPanel.SetActive(true);
    }

    public void SelectTarget(int pos) {
        dicePanel.SetActive(true);
        playDice.SetActive(true);
    }

    public void PlayDice() {
        //StartCoroutine(PlayDiceCorEachPerTime());
        StartCoroutine(PlayDiceCorAll());
    }

    //Versão de cada dado de uma vez
    IEnumerator PlayDiceCorEachPerTime() {
        foreach (var item in dices) {

            for (int i = 0; i < TypeDice; i++) {
                item.text = Random.Range(1, TypeDice).ToString();
                yield return new WaitForSeconds(speedDice);
            }

            //yield return new WaitForSeconds(1f);
            item.text = Random.Range(1, TypeDice).ToString();
        }
    }

    IEnumerator PlayDiceCorAll() {

        dices[0].text = Random.Range(1, TypeDice).ToString();
        dices[1].text = Random.Range(1, TypeDice).ToString();
        dices[2].text = Random.Range(1, TypeDice).ToString();
        yield return new WaitForSeconds(speedDice);

        dices[0].text = Random.Range(1, TypeDice).ToString();
        dices[1].text = Random.Range(1, TypeDice).ToString();
        dices[2].text = Random.Range(1, TypeDice).ToString();

    }
}
