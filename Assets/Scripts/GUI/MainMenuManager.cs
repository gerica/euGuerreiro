using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {
    [SerializeField] GameObject panelCreatePlayer;
    [SerializeField] GameObject panelListAdvantage;
    [SerializeField] GameObject listItemsAdvantage;
    [SerializeField] GameObject listAdvantageSelected;
    [SerializeField] GameObject panelListDisAdvantage;
    [SerializeField] GameObject listItemsDisAdvantage;
    [SerializeField] GameObject listDisAdvantageSelected;
    [SerializeField] Text[] textAttribute;
    [SerializeField] int pointsToSpend = 100;
    [SerializeField] Text pointsToSpendComp;
    [SerializeField] Image typeSex;
    [SerializeField] Button[] checkButtons;
    [SerializeField] Sprite[] checkSpriteButtons;
    [SerializeField] Sprite[] typesSpriteSex;
    [SerializeField] ItemAdvantage itemAdvPrefab;
    [SerializeField] ItemAdvantage itemDisAdvPrefab;
    [SerializeField] AdvSelected advSelectedPrefab;
    [SerializeField] AdvSelected disSelectedPrefab;
    [SerializeField] InputField inputFieldName;
    [SerializeField] GameObject btnContinuar;

    private PlayerData player;

    public static MainMenuManager Instance { get; set; }

    // Start is called before the first frame update
    void Start() {
        Instance = this;
        var initPos = 0;
        UpdatePointToSpendComp();
        typeSex.sprite = typesSpriteSex[initPos];
        checkButtons[initPos].image.sprite = checkSpriteButtons[1];
        checkButtons[1].image.sprite = checkSpriteButtons[0];
        LoadDataFile.CreateListAdvantage();
        LoadDataFile.CreateListDisadvantage();
        btnContinuar.SetActive(false);
        CheckSaveData();
    }

    // Update is called once per frame
    void Update() {

    }

    public void CheckSaveData() {
        //player = new Player();
        List<PlayerData> players = SaveData.LoadPlayers();
        if (players.Count > 0) {
            btnContinuar.SetActive(true);
            foreach (var i in players) {
                Debug.Log(i);
            }
        }
    }

    public void NewGame() {
        player = new PlayerData();
        panelCreatePlayer.SetActive(true);
    }

    public void Cancel() {
        panelCreatePlayer.SetActive(false);
    }

    public void SavePlayer() {
        player.NamePlayer = inputFieldName.text;
        player.St = Int32.Parse(textAttribute[0].text);
        player.Dx = Int32.Parse(textAttribute[1].text);
        player.Iq = Int32.Parse(textAttribute[2].text);
        player.Ht = Int32.Parse(textAttribute[3].text);

        SaveData.SavePlayer(player);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void UpdatePointToSpend(int value) {
        Debug.Log(value);
        pointsToSpend += value;
        UpdatePointToSpendComp();
    }

    public void UpdatePointToSpendComp() {
        pointsToSpendComp.text = pointsToSpend.ToString();
    }

    public void SelectTypeSex(int pos) {
        typeSex.sprite = typesSpriteSex[pos];
        if (pos == 0) {
            checkButtons[0].image.sprite = checkSpriteButtons[1];
            checkButtons[1].image.sprite = checkSpriteButtons[0];
            player.Sex = "M";
        } else {
            checkButtons[0].image.sprite = checkSpriteButtons[0];
            checkButtons[1].image.sprite = checkSpriteButtons[1];
            player.Sex = "F";
        }
    }

    public void PlusAttribute(int position) {
        int val = Int32.Parse(textAttribute[position].text);
        val++;
        textAttribute[position].text = val.ToString();
        switch (position) {
            case 0:
                UpdatePointToSpend(-10);
                break;
            case 1:
                UpdatePointToSpend(-10);
                break;
            case 2:
                UpdatePointToSpend(-20);
                break;
            case 3:
                UpdatePointToSpend(-20);
                break;
            default:
                break;
        }
    }

    public void MinusAttribute(int position) {
        int val = Int32.Parse(textAttribute[position].text);
        val--;
        textAttribute[position].text = val.ToString();
        switch (position) {
            case 0:
                UpdatePointToSpend(10);
                break;
            case 1:
                UpdatePointToSpend(10);
                break;
            case 2:
                UpdatePointToSpend(15);
                break;
            case 3:
                UpdatePointToSpend(15);
                break;
            default:
                break;
        }
    }

    public void OpenListAdvantage() {
        panelListAdvantage.SetActive(true);

        foreach (var item in LoadDataFile.listAdvantage) {
            bool flag = false;
            foreach (var advPlay in player.Advantages) {
                if (advPlay == item.Id) {
                    flag = true;
                    break;
                }
            }
            if (flag) {
                continue;
            }

            CreateItemAdvantageDisadvantage(item, listItemsAdvantage, itemAdvPrefab);
        }

    }


    public void CloseListAdvantage() {
        panelListAdvantage.SetActive(false);

        ItemAdvantage[] itemAdvantage = listItemsAdvantage.transform.GetComponentsInChildren<ItemAdvantage>();
        foreach (var item in itemAdvantage) {
            Destroy(item.gameObject);
        }
        MountPanelAdvantage();

    }


    public void MountPanelAdvantage() {
        AdvSelected[] advSelectedsLocal = listAdvantageSelected.transform.GetComponentsInChildren<AdvSelected>();

        foreach (var item in advSelectedsLocal) {
            Destroy(item.gameObject);
        }

        foreach (var idAdv in player.Advantages) {
            foreach (var adv in LoadDataFile.listAdvantage) {
                if (idAdv == adv.Id) {
                    CreateAdvSelected(adv, listAdvantageSelected, advSelectedPrefab);
                }
            }
        }
    }

    private DisadvantageAdvantage FindByAdvId(int idAdv) {
        foreach (var adv in LoadDataFile.listAdvantage) {
            if (idAdv == adv.Id) {
                return adv;
            }
        }
        return null;
    }
    private DisadvantageAdvantage FindByDisId(int idAdv) {
        foreach (var adv in LoadDataFile.listDisdvantage) {
            if (idAdv == adv.Id) {
                return adv;
            }
        }
        return null;
    }

    private void CreateItemAdvantageDisadvantage(DisadvantageAdvantage item, GameObject parent, ItemAdvantage prefab) {
        ItemAdvantage itemList = Instantiate(prefab);
        itemList.Id = item.Id;
        itemList.NameValue = item.Name;
        itemList.PointValue = item.PointInit.ToString() + " pontos";
        itemList.DescriptionValue = item.Description;
        itemList.transform.parent = parent.transform; // para adicionar no componente pai
        itemList.transform.localScale = new Vector3(1, 1, 0);
    }

    private void CreateAdvSelected(DisadvantageAdvantage item, GameObject parent, AdvSelected prefab) {
        AdvSelected itemList = Instantiate(prefab);
        itemList.Id = item.Id;
        itemList.NameValue = item.Name;
        itemList.transform.parent = parent.transform; // para adicionar no componente pai
        itemList.transform.localScale = new Vector3(1, 1, 0);
    }

    public void OpenListDisadvantage() {
        panelListDisAdvantage.SetActive(true);

        foreach (var item in LoadDataFile.listDisdvantage) {
            bool flag = false;
            foreach (var advPlay in player.Disadvantages) {
                if (advPlay == item.Id) {
                    flag = true;
                    break;
                }
            }
            if (flag) {
                continue;
            }

            CreateItemAdvantageDisadvantage(item, listItemsDisAdvantage, itemDisAdvPrefab);
        }

    }


    public void CloseListDisadvantage() {
        panelListDisAdvantage.SetActive(false);

        ItemAdvantage[] itemAdvantage = listItemsDisAdvantage.transform.GetComponentsInChildren<ItemAdvantage>();
        foreach (var item in itemAdvantage) {
            Destroy(item.gameObject);
        }
        MountPanelDisadvantage();

    }

    public void AddAdvantage(int id) {
        player.AddAdvantage(id);
        DisadvantageAdvantage adv = FindByAdvId(id);
        if (adv != null) {
            UpdatePointToSpend(-adv.PointInit);
        }
        CloseListAdvantage();
    }

    public void RemoveAdvantage(int id) {
        player.RemoveAdvantage(id);
        DisadvantageAdvantage adv = FindByAdvId(id);
        if (adv != null) {
            UpdatePointToSpend(adv.PointInit);
        }
        MountPanelAdvantage();
    }


    public void AddDisdvantage(int id) {
        player.AddDisadvantage(id);
        DisadvantageAdvantage adv = FindByDisId(id);
        if (adv != null) {
            UpdatePointToSpend(-adv.PointInit);
        }
        CloseListDisadvantage();
    }

    public void RemoveDisadvantage(int id) {
        player.RemoveDisadvantage(id);
        DisadvantageAdvantage adv = FindByDisId(id);
        if (adv != null) {
            UpdatePointToSpend(adv.PointInit);
        }
        MountPanelDisadvantage();
    }

    public void MountPanelDisadvantage() {
        AdvSelected[] advSelectedsLocal = listDisAdvantageSelected.transform.GetComponentsInChildren<AdvSelected>();

        foreach (var item in advSelectedsLocal) {
            Destroy(item.gameObject);
        }

        foreach (var idAdv in player.Disadvantages) {
            foreach (var adv in LoadDataFile.listDisdvantage) {
                if (idAdv == adv.Id) {
                    CreateAdvSelected(adv, listDisAdvantageSelected, disSelectedPrefab);
                }
            }
        }
    }


}
