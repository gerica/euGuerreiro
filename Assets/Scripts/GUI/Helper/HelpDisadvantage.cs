﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpDisadvantage : HelpGUI {
    private GameObject panelListDisAdvantage;
    private GameObject listItemsDisAdvantage;
    private GameObject listDisAdvantageSelected;
    private ItemAdvantage itemDisAdvPrefab;
    private AdvSelected disSelectedPrefab;
    private PlayerData player;

    public GameObject PanelListDisAdvantage { get => panelListDisAdvantage; set => panelListDisAdvantage = value; }
    public GameObject ListItemsDisAdvantage { get => listItemsDisAdvantage; set => listItemsDisAdvantage = value; }
    public GameObject ListDisAdvantageSelected { get => listDisAdvantageSelected; set => listDisAdvantageSelected = value; }
    public ItemAdvantage ItemDisAdvPrefab { get => itemDisAdvPrefab; set => itemDisAdvPrefab = value; }
    public AdvSelected DisSelectedPrefab { get => disSelectedPrefab; set => disSelectedPrefab = value; }
    public PlayerData Player { get => player; set => player = value; }

    public void OpenListDisadvantage() {
        panelListDisAdvantage.SetActive(true);

        foreach (var item in LoadDataFile.listDisdvantage) {
            bool flag = false;
            foreach (var advPlay in player.Disadvantages) {
                if (advPlay.Id == item.Id) {
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

    public void AddDisdvantage(int id) {
        DisadvantageAdvantage obj = FindByDisId(id);
        player.Disadvantages.Add(obj);
        if (obj != null) {
            UpdatePointToSpend(-obj.PointInit);
        }
        CloseListDisadvantage();
    }

    public void RemoveDisadvantage(int id) {
        DisadvantageAdvantage obj = FindByDisId(id);
        player.Disadvantages.Remove(obj);
        if (obj != null) {
            UpdatePointToSpend(obj.PointInit);
        }
        MountPanelDisadvantage();
    }

    public void MountPanelDisadvantage() {
        AdvSelected[] advSelectedsLocal = listDisAdvantageSelected.transform.GetComponentsInChildren<AdvSelected>();

        foreach (var item in advSelectedsLocal) {
            Destroy(item.gameObject);
        }

        foreach (var obj in player.Disadvantages) {
            CreateAdvSelected(obj, listDisAdvantageSelected, disSelectedPrefab);
        }
    }

    private DisadvantageAdvantage FindByDisId(int idAdv) {
        foreach (var adv in LoadDataFile.listDisdvantage) {
            if (idAdv == adv.Id) {
                return adv;
            }
        }
        return null;
    }
}
