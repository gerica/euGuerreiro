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
    [SerializeField] GameObject infoPanel;
    [SerializeField] Text txtInfo;
    [SerializeField] GameObject positionAttack;
    [SerializeField] DamageNumber damageNumber;
    [SerializeField] GameObject statusPanel;

    [SerializeField] float speedDice = 0.1f;
    private int typeDice = 7;
    private SortedList orderAction;
    private int currentTurn = -1;
    private int sizePlayerBattle = 0;
    private int sumDices = 0;
    private Skill skillSelected;
    private bool canAttack;
    private Vector3 positionOrigin;
    private int dicesActives = 1;
    private BattlePlayer currentPlayer;
    private BattlePlayer currentTargetPlayer;
    private bool canPlayDices = true;

    public static BattleManager Instance { get; set; }
    public int TypeDice { get => typeDice; set => typeDice = value; }

    // Start is called before the first frame update
    void Start() {
        Instance = this;
        actionPanel.SetActive(false);
        targetPanel.SetActive(false);
        dicePanel.SetActive(false);
        playDice.SetActive(false);
        infoPanel.SetActive(false);

    }

    // Update is called once per frame
    void Update() {
        //if (Input.GetKeyDown(KeyCode.T)) {
        //    BattleStart();
        //}
        if (Input.GetKeyDown(KeyCode.Y)) {
            BattleEnd();
        }

        //if (canAttack) {
        //    StartCoroutine(MoveToPositionAttack());
        //}
    }
    public void BattleStart(BattleEnemy enemy) {
        //Camera.main.orthographicSize = 6.0f;
        GameManager.Instance.BattleActive = true;
        battleScene.SetActive(false); // todo remover, pois a cena por padrão estara desabilitada
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

            //Vector3 pos = position.transform.po
            //playerBattle.transform.position = position.transform.position;
            PlayerData enemyData = EnemyFactory.crateEnemy(enemy.Enemies[i]);
            playerBattle.Player = enemyData;
            playerBattle.transform.SetParent(enemiesPositions[randomPosition].transform); // para adicionar no componente pai
            playerBattle.transform.localScale = new Vector3(1, 1, 0);
            sizePlayerBattle++;
        }
    }

    private void UpdateStatusPanel() {
        if (currentPlayer.Player.IsEnemy) {
            statusPanel.SetActive(false);
        } else {
            statusPanel.SetActive(true);
            foreach (Image img in statusPanel.GetComponentsInChildren<Image>()) {
                if (img.name == "imgPlayer") {
                    img.sprite = currentPlayer.GetSprite();
                    break;
                }
            }
            foreach (Text txt in statusPanel.GetComponentsInChildren<Text>()) {
                if (txt.name == "txtHT") {
                    txt.text = "HT: " + currentPlayer.Player.HtPlayer;
                    break;
                }
            }
        }
    }

    private void InitTurn() {
        // battleScene.SetActive(false);
        // actionPanel.SetActive(false);
        targetPanel.SetActive(false);
        dicePanel.SetActive(false);
        playDice.SetActive(false);
        infoPanel.SetActive(false);

        txtInfo.text = "";
        skillSelectedText.text = "";
        sumDices = 0;
        skillSelected = null;
        canAttack = false;
        positionOrigin = new Vector3(); ;
        dicesActives = 1;
        canPlayDices = true;
        currentPlayer = null;
        currentTargetPlayer = null;
        ResetDices();
    }

    public void NextTurn() {
        InitTurn();
        currentTurn++;
        if (currentTurn >= sizePlayerBattle) {
            currentTurn = 0;
        }
        //currentTurn++; // TODO para o inimgo ser o primeiro a jogar
        currentPlayer = orderAction.GetByIndex(currentTurn) as BattlePlayer;

        CreateShadowCurrentTurn();
        ActiveActionPanel();
        UpdateStatusPanel();
        //if (!currentPlay.Player.IsEnemy) {

        //} else {
        //    Debug.Log("enimigo atacar");
        //    CreateShadowCurrentTurn(enemiesPositions);
        //    ActiveActionPanel();
        //}
        //TurnWaiting = true;
        //UpdateBattle();
        //UpdateUIState();
    }

    private void CreateShadowCurrentTurn() {
        GameObject[] listPostPlayer;
        if (currentPlayer.Player.IsEnemy) {
            listPostPlayer = enemiesPositions;
        } else {
            listPostPlayer = playersPositions;
        }

        foreach (GameObject pos in listPostPlayer) {
            if (pos.GetComponentsInChildren<BattlePlayer>().Length > 0) {
                Vector3 vector3 = new Vector3(0.203f, 0.82f, 0);
                GameObject turnObject = Instantiate(currentTurnPrefab, pos.transform.position - vector3, pos.transform.rotation);
                turnObject.transform.SetParent(pos.transform); // para adicionar no componente pai
                turnObject.transform.localScale = new Vector3(1, 1, 0);
                break;
            }
        }
    }

    public void ActiveActionPanel() {
        actionPanel.SetActive(true);

        foreach (ActionSkillButton skillButton in actionPanel.GetComponentsInChildren<ActionSkillButton>()) {
            Destroy(skillButton.gameObject);
        }

        foreach (Skill skill in currentPlayer.Player.Skills) {
            ActionSkillButton buttonSkillAction = Instantiate(actionButtonsPrefab);
            buttonSkillAction.Skill = skill;

            buttonSkillAction.transform.SetParent(actionPanel.transform); // para adicionar no componente pai
            buttonSkillAction.transform.localScale = new Vector3(1, 1, 0);

            if (currentPlayer.Player.IsEnemy) {
                buttonSkillAction.GetComponentInChildren<Button>().interactable = false;
                StartCoroutine(ActionAttackEnemyCoroutine());
            }
        }
    }

    public void ActionAttack(Skill skill) {
        skillSelected = skill;
        targetPanel.SetActive(true);

        foreach (SelectTarget exist in targetPanel.GetComponentsInChildren<SelectTarget>()) {
            Destroy(exist.gameObject);
        }

        List<SelectTarget> listTargetPlayer = new List<SelectTarget>(); ;

        UpdatePanelSkillSelected();

        GameObject[] listTargets;
        if (currentPlayer.Player.IsEnemy) {
            listTargets = playersPositions;
        } else {
            listTargets = enemiesPositions;
        }

        foreach (GameObject pos in listTargets) {

            foreach (BattlePlayer battle in pos.GetComponentsInChildren<BattlePlayer>()) {
                SelectTarget selectTarget = Instantiate(namesTargetPrefab);
                selectTarget.Target = battle;
                selectTarget.transform.SetParent(targetPanel.transform); // para adicionar no componente pai
                selectTarget.transform.localScale = new Vector3(1, 1, 0);

                listTargetPlayer.Add(selectTarget);
            }
        }

        if (currentPlayer.Player.IsEnemy) {
            int index = UnityEngine.Random.Range(0, listTargetPlayer.Count);
            StartCoroutine(SelectTargetCoroutine(listTargetPlayer[index].Target));
        }

    }

    private IEnumerator SelectTargetCoroutine(BattlePlayer target) {
        yield return new WaitForSeconds(0.3f);
        SelectTarget(target);
        yield return new WaitForSeconds(0.3f);
        PlayDice();
    }

    public void SelectTarget(BattlePlayer target) {
        currentTargetPlayer = target;
        dicePanel.SetActive(true);
        playDice.SetActive(true);
        infoPanel.SetActive(true);
    }

    public void PlayDice() {
        //StartCoroutine(PlayDiceCorEachPerTime());
        if (canPlayDices) {
            canPlayDices = false;
            StartCoroutine(PlayDiceCorAll());
        }
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
        if (!canAttack) {
            dicesActives = 1;
            CheckSuccess();
        } else {
            CheckDamage(dice1, dice2, dice3);
        }

    }

    private void CheckDamage(int dice1, int dice2, int dice3) {
        int sumDices = 0;
        if (dicesActives == 1) {
            sumDices = dice1;
        } else if (dicesActives == 2) {
            sumDices = dice1 + dice2;
        }

        StartCoroutine(DoAttackWithDamage(sumDices));
    }

    private void UpdatePanelSkillSelected() {
        string nivel = "(" + skillSelected.Type + "-" + currentPlayer.Player.GetNivelSkill(skillSelected).ToString() + ")";
        skillSelectedText.text = skillSelected.Name + nivel;
    }

    private void CheckSuccess() {
        bool success = sumDices <= currentPlayer.Player.GetNivelSkill(skillSelected);
        StartCoroutine(UpdatePanelInfoActionCoroutine(success));
    }

    private IEnumerator UpdatePanelInfoActionCoroutine(bool success) {
        //yield return new WaitForSeconds(0.5f);
        if (success) {
            txtInfo.text = "Sucesso";
            yield return new WaitForSeconds(0.5f);
            canAttack = success;
            MoveToPositionAttack();
            canPlayDices = true;
        } else {
            txtInfo.text = "Falha";
            yield return new WaitForSeconds(0.8f);
            NextTurn();
        }

    }

    private void ResetDices() {
        dices[0].gameObject.SetActive(true);
        dices[1].gameObject.SetActive(true);
        dices[2].gameObject.SetActive(true);
        dices[0].text = "d6";
        dices[1].text = "d6";
        dices[2].text = "d6";
        canPlayDices = true;
    }

    public void MoveToPositionAttack() {
        positionOrigin = currentPlayer.transform.position;
        currentPlayer.transform.position = new Vector3(positionAttack.transform.position.x, positionAttack.transform.position.y, 0);
        //arraySuccessError[0].gameObject.SetActive(true);
        txtInfo.text = "Calcular dano";
        EnumDamange damage = TabelaDano.GetDemangeByST(currentPlayer.Player.St);

        if (damage.ToString().Contains("d1")) {
            dices[1].gameObject.SetActive(false);
            dices[2].gameObject.SetActive(false);
        } else if (damage.ToString().Contains("d2")) {
            dices[2].gameObject.SetActive(false);
            dicesActives = 2;
        }

        if (currentPlayer.Player.IsEnemy) {
            StartCoroutine(PlayDiceEnemyCoroutine());
        }
        //StartCoroutine(Attack(positionOrigin));
    }

    public void MoveToPositionDefault() {
        currentPlayer.transform.position = positionOrigin;
    }

    public IEnumerator DoAttackWithDamage(int damage) {
        Vector3 posArm;
        if (currentPlayer.Player.IsEnemy) {
            posArm = new Vector3(currentPlayer.transform.position.x + 1, currentPlayer.transform.position.y, currentPlayer.transform.position.z);
        } else {
            posArm = new Vector3(currentPlayer.transform.position.x - 1, currentPlayer.transform.position.y, currentPlayer.transform.position.z);

        }
        GameObject arm = Instantiate(currentPlayer.GetCurrentArmPrefab(skillSelected), posArm, currentPlayer.transform.rotation);
        yield return new WaitForSeconds(1f);

        canAttack = false;
        MoveToPositionDefault();
        Destroy(arm.gameObject);

        Instantiate(damageNumber, currentTargetPlayer.transform.position, currentTargetPlayer.transform.rotation).SetDamage(damage);
        currentTargetPlayer.Player.LoseHT(damage);
        NextTurn();
    }

    public IEnumerator ActionAttackEnemyCoroutine() {
        yield return new WaitForSeconds(0.5f);
        int index = UnityEngine.Random.Range(0, currentPlayer.Player.Skills.Count);
        Skill skill = currentPlayer.Player.Skills[index];
        ActionAttack(skill);
    }

    public IEnumerator PlayDiceEnemyCoroutine() {
        yield return new WaitForSeconds(0.5f);
        PlayDice();
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
        infoPanel.SetActive(false);
        UiFade.Instance.FadeFromBlack();
    }
}
