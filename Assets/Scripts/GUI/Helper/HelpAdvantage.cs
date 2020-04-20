using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpAdvantage : HelpGUI {
    private GameObject panelListAdvantage;
    private GameObject listItemsAdvantage;
    private GameObject listAdvantageSelected;
    private ItemAdvantage itemAdvPrefab;
    private AdvSelected advSelectedPrefab;
    private PlayerData player;

    public GameObject PanelListAdvantage { get => panelListAdvantage; set => panelListAdvantage = value; }
    public GameObject ListItemsAdvantage { get => listItemsAdvantage; set => listItemsAdvantage = value; }
    public GameObject ListAdvantageSelected { get => listAdvantageSelected; set => listAdvantageSelected = value; }
    public ItemAdvantage ItemAdvPrefab { get => itemAdvPrefab; set => itemAdvPrefab = value; }
    public AdvSelected AdvSelectedPrefab { get => advSelectedPrefab; set => advSelectedPrefab = value; }
    public PlayerData Player { get => player; set => player = value; }

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


    private void MountPanelAdvantage() {
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

    private DisadvantageAdvantage FindByAdvId(int idAdv) {
        foreach (var adv in LoadDataFile.listAdvantage) {
            if (idAdv == adv.Id) {
                return adv;
            }
        }
        return null;
    }
}
