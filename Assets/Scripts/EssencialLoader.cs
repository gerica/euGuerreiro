using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssencialLoader : MonoBehaviour {

    [SerializeField] GameObject UIScreen;
    [SerializeField] GameObject player;
    [SerializeField] GameObject gameManager;


    // Start is called before the first frame update
    void Start() {
        if (GameManager.Instance == null) {
            GameManager.Instance = Instantiate(gameManager).GetComponent<GameManager>();
        }

        if (UiFade.instance == null) {
            UiFade.instance = Instantiate(UIScreen).GetComponent<UiFade>();

        }
        if (PlayerController.Instance == null) {
            PlayerController clone = Instantiate(player).GetComponent<PlayerController>();
            PlayerController.Instance = clone;
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
