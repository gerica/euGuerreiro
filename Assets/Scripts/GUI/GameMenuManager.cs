using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour {
    [SerializeField] GameObject menuGame;
    [SerializeField] GameObject[] containers;
    [SerializeField] AdvSelected advSelectedPrefab;
    [SerializeField] AdvSelected disSelectedPrefab;
    [SerializeField] SkillSelected skillSelectedPrefab;
    [SerializeField] Sprite[] typesSpriteSex;

    public static GameMenuManager Instance { get; set; }
    // Start is called before the first frame update
    void Start() {
        Instance = this;
        //LoadDataFile.CreateListAdvantage();
        //LoadDataFile.CreateListDisadvantage();
        //LoadDataFile.CreateListSkills();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire2")) {
            if (menuGame.activeInHierarchy) {
                CloseMenu();
            } else {
                menuGame.SetActive(true);
                OpenMenuStatus();
                //UpdateMainStats();
                GameManager.Instance.GameMenuOpen = true;
            }
        }
    }

    public void CloseMenu() {
        //Debug.Log("Fechar Menu");
        //for (int i = 0; i < windows.Length; i++) {
        //    windows[i].SetActive(false);
        //}
        menuGame.SetActive(false);
        GameManager.Instance.GameMenuOpen = false;
        //itemCharChoiceMenu.SetActive(false);
    }

    public void OpenMenuStatus() {
        for (int i = 0; i < containers.Length; i++) {
            var container = containers[i];
            if (i == 0) {
                container.SetActive(true);
                UpdateContainerStatus(container);
            } else {
                container.SetActive(false);
            }
        }
    }

    public void OpenMenuItem() {
        for (int i = 0; i < containers.Length; i++) {
            var container = containers[i];
            if (i == 1) {
                container.SetActive(true);
            } else {
                container.SetActive(false);
            }
        }
    }

    private void UpdateContainerStatus(GameObject container) {
        PlayerData player = PlayerController.Instance.PlayerData;
        UpdateContainerInfo(container, player);

        GridLayoutGroup[] grids = container.GetComponentsInChildren<GridLayoutGroup>();

        foreach (GridLayoutGroup grid in grids) {
            MountAdvantage(player, grid);
            MountDisadvantage(player, grid);
            MountListSkills(container, player);
            MounyImgSex(container, player);
        }
    }

    private void MounyImgSex(GameObject container, PlayerData player) {
        Image[] images = container.GetComponentsInChildren<Image>();
        foreach (Image image in images) {
            if (image.name == EnumContainerStatus.typeSex.ToString()) {
                if (EnumSex.M == player.Sex) {
                    image.sprite = typesSpriteSex[0];
                } else {
                    image.sprite = typesSpriteSex[1];
                }
                break;
            }
        }
    }

    private static void UpdateContainerInfo(GameObject container, PlayerData player) {
        Text[] texts = container.GetComponentsInChildren<Text>();
        foreach (Text t in texts) {
            if (t.name == EnumContainerStatus.txtName.ToString()) {
                t.text = player.NamePlayer;
            }
            if (t.name == EnumContainerStatus.txtValueST.ToString()) {
                t.text = player.St.ToString();
            }
            if (t.name == EnumContainerStatus.txtValueDX.ToString()) {
                t.text = player.Dx.ToString();
            }
            if (t.name == EnumContainerStatus.txtValueIQ.ToString()) {
                t.text = player.Iq.ToString();
            }
            if (t.name == EnumContainerStatus.txtValueHT.ToString()) {
                t.text = player.Ht.ToString();
            }
        }
    }

    private void MountAdvantage(PlayerData player, GridLayoutGroup grid) {
        if (grid.name == EnumContainerStatus.listAdvSelected.ToString()) {

            AdvSelected[] advSelectedsLocal = grid.transform.GetComponentsInChildren<AdvSelected>();

            foreach (var item in advSelectedsLocal) {
                Destroy(item.gameObject);
            }

            foreach (var adv in player.Advantages) {
                AdvSelected itemList = Instantiate(advSelectedPrefab);
                itemList.Id = adv.Id;
                itemList.NameValue = adv.Name;
                itemList.transform.SetParent(grid.transform); // para adicionar no componente pai
                itemList.transform.localScale = new Vector3(1, 1, 0);
            }
        }
    }

    private void MountDisadvantage(PlayerData player, GridLayoutGroup grid) {
        if (grid.name == EnumContainerStatus.listDisSelected.ToString()) {

            AdvSelected[] advSelectedsLocal = grid.transform.GetComponentsInChildren<AdvSelected>();

            foreach (var item in advSelectedsLocal) {
                Destroy(item.gameObject);
            }

            foreach (var obj in player.Disadvantages) {
                AdvSelected itemList = Instantiate(disSelectedPrefab);
                itemList.Id = obj.Id;
                itemList.NameValue = obj.Name;
                itemList.transform.SetParent(grid.transform); // para adicionar no componente pai
                itemList.transform.localScale = new Vector3(1, 1, 0);
            }
        }
    }

    private void MountListSkills(GameObject container, PlayerData player) {

        VerticalLayoutGroup[] verticalLayouts = container.transform.GetComponentsInChildren<VerticalLayoutGroup>();

        foreach (var list in verticalLayouts) {
            if (list.name == EnumContainerStatus.listSkillsSelected.ToString()) {
                SkillSelected[] items = list.transform.GetComponentsInChildren<SkillSelected>();

                foreach (var item in items) {
                    Destroy(item.gameObject);
                }

                foreach (var item in player.Skills) {
                    //Debug.Log(idAdv);
                    SkillSelected itemList = Instantiate(skillSelectedPrefab);
                    itemList.TxtSkillValue = item.Name;
                    itemList.TxtNivelValue = item.Type + "-" + player.GetNivelSkill(item).ToString();
                    itemList.transform.SetParent(list.transform); // para adicionar no componente pai
                    itemList.transform.localScale = new Vector3(1, 1, 0);
                    //break;
                }
            }
        }
    }
}
