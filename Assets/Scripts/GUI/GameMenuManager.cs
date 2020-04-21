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

    public static GameMenuManager Instance { get; set; }
    // Start is called before the first frame update
    void Start() {
        Instance = this;
        LoadDataFile.CreateListAdvantage();
        LoadDataFile.CreateListDisadvantage();
        LoadDataFile.CreateListSkills();
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
        Debug.Log("Chamou");
        PlayerData player = PlayerController.Instance.PlayerData;
        UpdateContainerInfo(container, player);

        GridLayoutGroup[] grids = container.GetComponentsInChildren<GridLayoutGroup>();

        foreach (GridLayoutGroup grid in grids) {
            Debug.Log(grid);
            MountAdvantage(player, grid);
            MountDisadvantage(player, grid);
            MountListSkills(container, player);
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

            foreach (var idAdv in player.Advantages) {
                //Debug.Log(idAdv);
                foreach (var adv in LoadDataFile.listAdvantage) {
                    if (idAdv == adv.Id) {
                        AdvSelected itemList = Instantiate(advSelectedPrefab);
                        itemList.Id = adv.Id;
                        itemList.NameValue = adv.Name;
                        itemList.transform.SetParent(grid.transform); // para adicionar no componente pai
                        itemList.transform.localScale = new Vector3(1, 1, 0);
                        break;
                    }
                }
            }
        }
    }

    private void MountDisadvantage(PlayerData player, GridLayoutGroup grid) {
        if (grid.name == EnumContainerStatus.listDisSelected.ToString()) {

            AdvSelected[] advSelectedsLocal = grid.transform.GetComponentsInChildren<AdvSelected>();

            foreach (var item in advSelectedsLocal) {
                Destroy(item.gameObject);
            }

            foreach (var idAdv in player.Disadvantages) {
                foreach (var adv in LoadDataFile.listDisdvantage) {
                    if (idAdv == adv.Id) {
                        Debug.Log(idAdv);
                        AdvSelected itemList = Instantiate(disSelectedPrefab);
                        itemList.Id = adv.Id;
                        itemList.NameValue = adv.Name;
                        itemList.transform.SetParent(grid.transform); // para adicionar no componente pai
                        itemList.transform.localScale = new Vector3(1, 1, 0);
                        break;
                    }
                }
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
