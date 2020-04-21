using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueManager : MonoBehaviour {
    [Header("Configurações")]
    [SerializeField] GameObject listPlayers;
    [SerializeField] InfoPlayer infoPlayerPrefab;
    // Start is called before the first frame update
    void Start() {
        List<PlayerData> list = SaveData.LoadPlayers();
        foreach (PlayerData player in list) {
            CreateItemList(player);
        }

    }

    // Update is called once per frame
    void Update() {

    }

    public void Cancel() {
        SceneManager.LoadScene(EnumScenes.MainMenu.ToString());
    }

    protected void CreateItemList(PlayerData player) {
        InfoPlayer itemList = Instantiate(infoPlayerPrefab);
        itemList.TimeGameValue = "00:00:00";
        itemList.Player = player;
        itemList.transform.SetParent(listPlayers.transform); // para adicionar no componente pai
        itemList.transform.localScale = new Vector3(1, 1, 0);
    }
}
