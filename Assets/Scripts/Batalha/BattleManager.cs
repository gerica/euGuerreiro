//using System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour {
    [SerializeField] GameObject battleScene;
    [SerializeField] GameObject actionPanel;
    [SerializeField] ActionSkillButton actionButtonsPrefab;
    [SerializeField] GameObject targetPanel;
    [SerializeField] GameObject dicePanel;
    [SerializeField] GameObject playDice;
    [SerializeField] GameObject[] playersPositions;
    [SerializeField] GameObject[] enemiesPositions;
    [SerializeField] BattlePlayer maleBattlePlayerPrefab;
    [SerializeField] BattlePlayer femaleBattlePlayerPrefab;
    [SerializeField] Text[] dices;
    [SerializeField] SelectTarget namesTargetPrefab;
    [SerializeField] GameObject currentTurnPrefab;
    [SerializeField] Text skillSelectedText;

    [SerializeField] float speedDice = 0.1f;

    private int typeDice = 7;
    private SortedList orderAction;
    private int currentTurn = -1;
    private int sizePlayerBattle = 0;
    private int sumDices = 0;
    private Skill skillSelected;

    public static BattleManager Instance { get; set; }
    public int TypeDice { get => typeDice; set => typeDice = value; }

    // Start is called before the first frame update
    void Start() {
        Instance = this;
        actionPanel.SetActive(false);
        targetPanel.SetActive(false);
        dicePanel.SetActive(false);
        playDice.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        //if (Input.GetKeyDown(KeyCode.T)) {
        //    BattleStart();
        //}
        if (Input.GetKeyDown(KeyCode.Y)) {
            BattleEnd();
        }
    }

    public void BattleStart(BattleEnemy enemy) {
        //Camera.main.orthographicSize = 6.0f;
        GameManager.Instance.BattleActive = true;

        battleScene.SetActive(false); // todo remover, pois a cena por pad'rao estara desabilitada
        StartCoroutine(StartBattleCoroutine(enemy));
    }

    private IEnumerator StartBattleCoroutine(BattleEnemy enemy) {
        //yield return new WaitForSeconds(0.5f);
        UiFade.Instance.FadeToBlack();
        yield return new WaitForSeconds(1f);

        battleScene.SetActive(true);
        transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);

        InitPlayerPosition();
        InitEnemiesPosition(enemy);
        CalculateOrderAction();
        UiFade.Instance.FadeFromBlack();
        NextTurn();
    }

    private void CalculateOrderAction() {
        BattlePlayer[] players = this.GetComponentsInChildren<BattlePlayer>();
        var comparer = Comparer<float>.Create((x, y) => y.CompareTo(x));
        orderAction = new SortedList(comparer);
        foreach (BattlePlayer battle in players) {
            orderAction.Add(battle.Player.SpeedBasic, battle);
        }

        //foreach (DictionaryEntry pair in orderAction) {
        //    Debug.Log("key: " + pair.Key + " value: " + pair.Value);
        //}

    }

    private void InitPlayerPosition() {
        int randomPosition = UnityEngine.Random.Range(0, playersPositions.Length);
        GameObject position = playersPositions[randomPosition];
        BattlePlayer playerBattle;
        if (PlayerController.Instance.PlayerData.Sex == EnumSex.M) {
            playerBattle = Instantiate(maleBattlePlayerPrefab, position.transform.position, position.transform.rotation);
        } else {
            playerBattle = Instantiate(femaleBattlePlayerPrefab, position.transform.position, position.transform.rotation);
        }
        playerBattle.Player = PlayerController.Instance.PlayerData;
        playerBattle.transform.SetParent(playersPositions[randomPosition].transform); // para adicionar no componente pai
        playerBattle.transform.localScale = new Vector3(1, 1, 0);
        sizePlayerBattle++;
    }

    private void InitEnemiesPosition(BattleEnemy enemy) {

        for (int i = 0; i < enemy.EnemiesPrefab.Length; i++) {

            BattlePlayer enemyPrefab = enemy.EnemiesPrefab[i];
            int randomPosition = UnityEngine.Random.Range(0, enemiesPositions.Length);
            GameObject position = enemiesPositions[randomPosition];
            BattlePlayer playerBattle;
            playerBattle = Instantiate(enemyPrefab, position.transform.position, position.transform.rotation);
            PlayerData enemyData = EnemyFactory.crateEnemy(enemy.Enemies[i]);
            playerBattle.Player = enemyData;
            playerBattle.transform.SetParent(enemiesPositions[randomPosition].transform); // para adicionar no componente pai
            playerBattle.transform.localScale = new Vector3(1, 1, 0);
            sizePlayerBattle++;
        }
    }

    public void NextTurn() {
        currentTurn++;
        if (currentTurn >= sizePlayerBattle) {
            currentTurn = 0;
        }
        BattlePlayer player = orderAction.GetByIndex(currentTurn) as BattlePlayer;
        if (!player.Player.IsEnemy) {
            foreach (GameObject pos in playersPositions) {
                if (pos.GetComponentsInChildren<BattlePlayer>().Length > 0) {
                    ActiveActionPanel(player);
                    Vector3 vector3 = new Vector3(0.203f, 0.82f, 0);
                    GameObject turnObject = Instantiate(currentTurnPrefab, pos.transform.position - vector3, pos.transform.rotation);
                    turnObject.transform.SetParent(pos.transform); // para adicionar no componente pai
                    turnObject.transform.localScale = new Vector3(1, 1, 0);
                    break;
                }
            }

        } else {

        }
        //TurnWaiting = true;
        //UpdateBattle();
        //UpdateUIState();
    }

    public void ActiveActionPanel(BattlePlayer currentPlayer) {
        actionPanel.SetActive(true);
        foreach (Skill skill in currentPlayer.Player.Skills) {
            ActionSkillButton newObj = Instantiate(actionButtonsPrefab);
            newObj.Skill = skill;

            newObj.transform.SetParent(actionPanel.transform); // para adicionar no componente pai
            newObj.transform.localScale = new Vector3(1, 1, 0);
        }
    }

    private void BattleEnd() {
        StartCoroutine(BattleEndCoroutine());
    }

    private IEnumerator BattleEndCoroutine() {
        UiFade.Instance.FadeToBlack();
        yield return new WaitForSeconds(1f);
        foreach (GameObject pos in playersPositions) {
            foreach (BattlePlayer battle in pos.GetComponentsInChildren<BattlePlayer>()) {
                Destroy(battle.gameObject);
            }
        }

        foreach (GameObject pos in enemiesPositions) {
            foreach (BattlePlayer battle in pos.GetComponentsInChildren<BattlePlayer>()) {
                Destroy(battle.gameObject);
            }
        }

        //Camera.main.orthographicSize = 12.0f;
        GameManager.Instance.BattleActive = false;
        actionPanel.SetActive(false);
        battleScene.SetActive(false);
        targetPanel.SetActive(false);
        dicePanel.SetActive(false);
        playDice.SetActive(false);
        UiFade.Instance.FadeFromBlack();
    }

    public void ActionAttack(Skill skill) {
        skillSelected = skill;
        UpdatePanelSkillSelected();
        foreach (SelectTarget exist in targetPanel.GetComponentsInChildren<SelectTarget>()) {
            Destroy(exist.gameObject);
        }

        targetPanel.SetActive(true);
        foreach (GameObject pos in enemiesPositions) {
            foreach (BattlePlayer battle in pos.GetComponentsInChildren<BattlePlayer>()) {
                SelectTarget selectTarget = Instantiate(namesTargetPrefab);
                selectTarget.Target = battle;
                selectTarget.transform.SetParent(targetPanel.transform); // para adicionar no componente pai
                selectTarget.transform.localScale = new Vector3(1, 1, 0);
            }
        }
    }

    public void SelectTarget(BattlePlayer target) {
        Debug.Log("Atacar o " + target.Player.NamePlayer);
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
                item.text = UnityEngine.Random.Range(1, TypeDice).ToString();
                yield return new WaitForSeconds(speedDice);
            }

            //yield return new WaitForSeconds(1f);
            item.text = UnityEngine.Random.Range(1, TypeDice).ToString();
        }
    }

    IEnumerator PlayDiceCorAll() {

        for (int i = 0; i < 5; i++) {
            dices[0].text = UnityEngine.Random.Range(1, TypeDice).ToString();
            dices[1].text = UnityEngine.Random.Range(1, TypeDice).ToString();
            dices[2].text = UnityEngine.Random.Range(1, TypeDice).ToString();
            yield return new WaitForSeconds(speedDice);
        }
        int dice1 = UnityEngine.Random.Range(1, TypeDice);
        int dice2 = UnityEngine.Random.Range(1, TypeDice);
        int dice3 = UnityEngine.Random.Range(1, TypeDice);

        dices[0].text = dice1.ToString();
        dices[1].text = dice2.ToString();
        dices[2].text = dice3.ToString();

        sumDices = dice1 + dice2 + dice3;
        CheckSuccess();

    }

    private void UpdatePanelSkillSelected() {
        skillSelectedText.text = skillSelected.Name;
    }

    private void CheckSuccess() {
        BattlePlayer player = orderAction.GetByIndex(currentTurn) as BattlePlayer;
        foreach (Skill skill in player.Player.Skills) {
            if (skill == skillSelected) {
                Debug.Log(sumDices);
                Debug.Log(player.Player.GetNivelSkill(skillSelected));
                if (sumDices <= player.Player.GetNivelSkill(skillSelected)) {
                    Debug.Log("Acertou");
                } else {
                    Debug.Log("Errou");
                }
                break;
            }
        }
    }
}
