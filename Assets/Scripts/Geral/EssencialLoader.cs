using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssencialLoader : MonoBehaviour {

    [SerializeField] GameObject UIScreen;
    [SerializeField] GameObject[] players;
    [SerializeField] GameObject gameManager;


    // Start is called before the first frame update
    void Start() {
        if (GameManager.Instance == null) {
            GameManager.Instance = Instantiate(gameManager).GetComponent<GameManager>();
        }

        if (UiFade.Instance == null) {
            UiFade.Instance = Instantiate(UIScreen).GetComponent<UiFade>();

        }
        if (PlayerController.Instance == null) {
            //PlayerData playerData = SaveData.LoadPlayerByName(Session.Player.NamePlayer);
            PlayerData playerData = SaveData.LoadPlayerByName("Lais");
            PlayerController clone;
            if (EnumSex.M == playerData.Sex) {
                clone = Instantiate(players[0]).GetComponent<PlayerController>();
            } else {
                clone = Instantiate(players[1]).GetComponent<PlayerController>();
            }
            clone.PlayerData = playerData;
            PlayerController.Instance = clone;
        }
    }

}
