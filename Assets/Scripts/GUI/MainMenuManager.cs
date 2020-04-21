using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
    [Header("Configurações painel central")]
    [SerializeField] GameObject panelCreatePlayer;

    [Space]
    [Header("Configurações vantagens")]
    [SerializeField] GameObject panelListAdvantage;
    [SerializeField] GameObject listItemsAdvantage;
    [SerializeField] GameObject listAdvantageSelected;
    [SerializeField] ItemAdvantage itemAdvPrefab;
    [SerializeField] AdvSelected advSelectedPrefab;
    private HelpAdvantage helpAdv;

    [Space]
    [Header("Configurações desvantagens")]
    [SerializeField] GameObject panelListDisAdvantage;
    [SerializeField] GameObject listItemsDisAdvantage;
    [SerializeField] GameObject listDisAdvantageSelected;
    [SerializeField] ItemAdvantage itemDisAdvPrefab;
    [SerializeField] AdvSelected disSelectedPrefab;
    private HelpDisadvantage helpDisAdv;

    [Space]
    [Header("Configurações perícias")]
    [SerializeField] GameObject panelListSkills;
    [SerializeField] GameObject listItemsSkills;
    [SerializeField] GameObject listSkillsSelected;
    [SerializeField] ItemSkill itemSkillPrefab;
    [SerializeField] Text textConstTotalSkills;
    [SerializeField] SkillSelected skillSelectedPrefab;
    private HelpSkill helpSkill;

    [Space]
    [Header("Atributos")]
    [SerializeField] Text[] textAttribute;

    [Space]
    [Header("Geral")]
    [SerializeField] int pointsToSpend = 100;
    [SerializeField] Text pointsToSpendComp;
    [SerializeField] Image typeSex;
    [SerializeField] Button[] checkButtons;
    [SerializeField] Sprite[] checkSpriteButtons;
    [SerializeField] Sprite[] typesSpriteSex;
    [SerializeField] InputField inputFieldName;
    [SerializeField] GameObject btnContinuar;
    [SerializeField] GameObject panelWarning;

    private HelpPoints helpPoints;
    private PlayerData player;

    public static MainMenuManager Instance { get; set; }

    // Start is called before the first frame update
    void Start() {
        Instance = this;
        btnContinuar.SetActive(false);
        CheckSaveData();

    }

    private void ConfigureHelpPoints() {
        helpPoints = new HelpPoints();
        helpPoints.PointsToSpend = pointsToSpend;
        helpPoints.PointsToSpendComp = pointsToSpendComp;
    }

    private void ConfigureHelpAdvantage() {
        helpAdv = this.gameObject.AddComponent<HelpAdvantage>();
        helpAdv.HelpPoints = helpPoints;
        helpAdv.Player = player;

        helpAdv.ListAdvantageSelected = listAdvantageSelected;
        helpAdv.PanelListAdvantage = panelListAdvantage;
        helpAdv.ListItemsAdvantage = listItemsAdvantage;
        helpAdv.ItemAdvPrefab = itemAdvPrefab;
        helpAdv.AdvSelectedPrefab = advSelectedPrefab;
    }

    private void ConfigureHelpDisadvantage() {
        helpDisAdv = this.gameObject.AddComponent<HelpDisadvantage>();
        helpDisAdv.HelpPoints = helpPoints;
        helpDisAdv.Player = player;

        helpDisAdv.ListDisAdvantageSelected = listDisAdvantageSelected;
        helpDisAdv.PanelListDisAdvantage = panelListDisAdvantage;
        helpDisAdv.ListItemsDisAdvantage = listItemsDisAdvantage;
        helpDisAdv.ItemDisAdvPrefab = itemDisAdvPrefab;
        helpDisAdv.DisSelectedPrefab = disSelectedPrefab;
    }


    private void ConfigureHelpSkill() {
        helpSkill = this.gameObject.AddComponent<HelpSkill>();
        helpSkill.HelpPoints = helpPoints;
        helpSkill.Player = player;

        helpSkill.ListSkillsSelected = listSkillsSelected;
        helpSkill.PanelListSkills = panelListSkills;
        helpSkill.ListItemsSkills = listItemsSkills;
        helpSkill.ItemPrefab = itemSkillPrefab;
        helpSkill.ConstTotalSkills = textConstTotalSkills;
        helpSkill.SkillSelectedPrefab = skillSelectedPrefab;

    }

    public void CheckSaveData() {
        if (SaveData.IsLoadPlayers()) {
            btnContinuar.SetActive(true);
        }
    }

    public void NewGame() {
        InitPlayer();
        InitGUI();
    }

    public void ContinueGame() {
        SceneManager.LoadScene(EnumScenes.ContinueGame.ToString());
        //Destroy(gameObject);
    }

    private void InitPlayer() {
        player = new PlayerData();
        panelCreatePlayer.SetActive(true);
        player.St = Int32.Parse(textAttribute[0].text);
        player.Dx = Int32.Parse(textAttribute[1].text);
        player.Iq = Int32.Parse(textAttribute[2].text);
        player.Ht = Int32.Parse(textAttribute[3].text);
    }

    private void InitGUI() {
        ConfigureHelpPoints();
        ConfigureHelpAdvantage();
        ConfigureHelpDisadvantage();
        ConfigureHelpSkill();
        var initPos = 0;
        helpPoints.UpdatePointToSpendComp();
        typeSex.sprite = typesSpriteSex[initPos];
        checkButtons[initPos].image.sprite = checkSpriteButtons[1];
        checkButtons[1].image.sprite = checkSpriteButtons[0];
        LoadDataFile.CreateListAdvantage();
        LoadDataFile.CreateListDisadvantage();
        LoadDataFile.CreateListSkills();
    }

    public void Cancel() {
        panelCreatePlayer.SetActive(false);
    }

    public void SavePlayer() {
        player.NamePlayer = inputFieldName.text;
        if (ValidSavePlayer()) {
            SaveData.SavePlayer(player);
            Session.Player = player;
            SceneManager.LoadScene(EnumScenes.IntroGame.ToString());
        } else {
            OpenPanelWarning();
        }
    }


    private bool ValidSavePlayer() {
        if (player.NamePlayer == null || player.NamePlayer == "") {
            return false;
        }

        if (player.Advantages.Count == 0) {
            return false;
        }

        if (player.Disadvantages.Count == 0) {
            return false;
        }

        if (player.Skills.Count == 0) {
            return false;
        }
        return true;
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void OpenPanelWarning() {
        panelWarning.SetActive(true);
    }

    public void ClosePanelWarning() {
        panelWarning.SetActive(false);
    }

    public void SelectTypeSex(int pos) {
        typeSex.sprite = typesSpriteSex[pos];
        if (pos == 0) {
            checkButtons[0].image.sprite = checkSpriteButtons[1];
            checkButtons[1].image.sprite = checkSpriteButtons[0];
            player.Sex = EnumSex.M;
        } else {
            checkButtons[0].image.sprite = checkSpriteButtons[0];
            checkButtons[1].image.sprite = checkSpriteButtons[1];
            player.Sex = EnumSex.F;
        }
    }

    public void PlusAttribute(int position) {
        switch (position) {
            case 0: // ST
                player.St++;
                textAttribute[position].text = player.St.ToString();
                helpPoints.UpdatePointToSpend(-10);
                break;
            case 1: // DX
                player.Dx++;
                textAttribute[position].text = player.Dx.ToString();
                helpPoints.UpdatePointToSpend(-10);
                break;
            case 2: // IQ
                player.Iq++;
                textAttribute[position].text = player.Iq.ToString();
                helpPoints.UpdatePointToSpend(-20);
                break;
            case 3: // HT
                player.Ht++;
                textAttribute[position].text = player.Ht.ToString();
                helpPoints.UpdatePointToSpend(-20);
                break;
            default:
                break;
        }
        helpSkill.MountPanelItemSelected();
    }

    public void MinusAttribute(int position) {
        switch (position) {
            case 0: // ST
                player.St--;
                textAttribute[position].text = player.St.ToString();
                helpPoints.UpdatePointToSpend(10);
                break;
            case 1: // DX
                player.Dx--;
                textAttribute[position].text = player.Dx.ToString();
                helpPoints.UpdatePointToSpend(10);
                break;
            case 2: // IQ
                player.Iq--;
                textAttribute[position].text = player.Iq.ToString();
                helpPoints.UpdatePointToSpend(15);
                break;
            case 3: // HT
                player.Ht--;
                textAttribute[position].text = player.Ht.ToString();
                helpPoints.UpdatePointToSpend(15);
                break;
            default:
                break;
        }
        helpSkill.MountPanelItemSelected();
    }

    public void OpenListAdvantage() {
        helpAdv.OpenListAdvantage();
    }

    public void CloseListAdvantage() {
        helpAdv.CloseListAdvantage();
    }

    public void AddAdvantage(int id) {
        helpAdv.AddAdvantage(id);
    }

    public void RemoveAdvantage(int id) {
        helpAdv.RemoveAdvantage(id);
    }

    public void OpenListDisadvantage() {
        helpDisAdv.OpenListDisadvantage();
    }

    public void CloseListDisadvantage() {
        helpDisAdv.CloseListDisadvantage();
    }

    public void AddDisdvantage(int id) {
        helpDisAdv.AddDisdvantage(id);
    }

    public void RemoveDisadvantage(int id) {
        helpDisAdv.RemoveDisadvantage(id);
    }

    public void OpenListSkills() {
        helpSkill.OpenList();
        //panelListSkills.SetActive(true);
    }

    public void CloseListSkills() {
        helpSkill.CloseList();
    }

    public void ConfirmListSkills() {
        helpSkill.ConfirmChooseSkills();
    }

    public void UpdatePoints() {
        helpSkill.UpdatePanelPoints();
    }

}
