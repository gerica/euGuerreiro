using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpSkill : HelpGUI {
    private GameObject panelListSkills;
    private GameObject listItemsSkills;
    private GameObject listSkillsSelected;
    private ItemSkill itemPrefab;
    private SkillSelected skillSelectedPrefab;
    private PlayerData player;
    private List<Skill> listSkill = new List<Skill>();
    private Text constTotalSkills;

    public GameObject PanelListSkills { get => panelListSkills; set => panelListSkills = value; }
    public GameObject ListItemsSkills { get => listItemsSkills; set => listItemsSkills = value; }
    public GameObject ListSkillsSelected { get => listSkillsSelected; set => listSkillsSelected = value; }
    public ItemSkill ItemPrefab { get => itemPrefab; set => itemPrefab = value; }
    public PlayerData Player { get => player; set => player = value; }
    public List<Skill> ListSkill { get => listSkill; set => listSkill = value; }
    public Text ConstTotalSkills { get => constTotalSkills; set => constTotalSkills = value; }
    public SkillSelected SkillSelectedPrefab { get => skillSelectedPrefab; set => skillSelectedPrefab = value; }

    public void OpenList() {
        PanelListSkills.SetActive(true);

        foreach (var item in LoadDataFile.listSkills) {
            Skill skillAlreadyAdded = null;
            foreach (var skill in Player.Skills) {
                if (skill.Id == item.Id) {
                    skillAlreadyAdded = skill;
                    break;
                }
            }

            CreateItem(item, skillAlreadyAdded);
            UpdatePanelPoints();
        }

    }

    public void CloseList() {
        PanelListSkills.SetActive(false);

        ItemSkill[] itemAdvantage = ListItemsSkills.transform.GetComponentsInChildren<ItemSkill>();
        foreach (var item in itemAdvantage) {
            Destroy(item.gameObject);
        }

        MountPanelItemSelected();
    }

    public void UpdatePanelPoints() {
        int total = CalculateTotalSpent();
        constTotalSkills.text = total.ToString();

    }

    private int CalculateTotalSpent() {
        ItemSkill[] items = ListItemsSkills.transform.GetComponentsInChildren<ItemSkill>();
        int total = 0;
        foreach (var skill in items) {
            //Destroy(item.gameObject);
            total += skill.TotalCost;
        }

        return total;
    }

    public void ConfirmChooseSkills() {
        int total = CalculateTotalSpent();
        
        HelpPoints.PointsToSpendSkill = total;

        player.Skills = new List<Skill>();
        ItemSkill[] items = ListItemsSkills.transform.GetComponentsInChildren<ItemSkill>();
        foreach (var itemSkill in items) {
            if (itemSkill.Count > 0) {
                foreach (var skill in LoadDataFile.listSkills) {
                    if (skill.Id == itemSkill.Id) {
                        skill.Nivel = itemSkill.Count;
                        player.Skills.Add(skill);
                    }
                }
            }
        }
        CloseList();
        UpdatePointToSpendComp();
    }

    public void MountPanelItemSelected() {
        SkillSelected[] items = ListSkillsSelected.transform.GetComponentsInChildren<SkillSelected>();

        foreach (var item in items) {
            Destroy(item.gameObject);
        }

        foreach (var skill in player.Skills) {
            CreateShillSelected(skill);
        }
    }

    private void CreateItem(Skill item, Skill skillAlreadyAdded) {
        ItemSkill itemList = Instantiate(itemPrefab);
        itemList.Id = item.Id;
        itemList.NameValue = item.Name;
        itemList.DifficultValue = item.Type + "/" + item.Difficult;
        itemList.DescriptionValue = item.Description;
        if (skillAlreadyAdded != null) {
            itemList.Count = skillAlreadyAdded.Nivel;
            itemList.CalculeteCost();
        }

        itemList.transform.SetParent(ListItemsSkills.transform); // para adicionar no componente pai
        itemList.transform.localScale = new Vector3(1, 1, 0);
    }

    protected void CreateShillSelected(Skill item) {
        SkillSelected itemList = Instantiate(skillSelectedPrefab);
        itemList.TxtSkillValue = item.Name;
        itemList.TxtNivelValue = item.Type + "-" + player.GetNivelSkill(item).ToString();
        itemList.transform.SetParent(listSkillsSelected.transform); // para adicionar no componente pai
        itemList.transform.localScale = new Vector3(1, 1, 0);
    }

}
