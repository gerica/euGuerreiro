using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : MonoBehaviour {

    [SerializeField] GameObject player;

    public GameObject Player { get => player; set => player = value; }

    // Start is called before the first frame update
    void Start() {
        if (PlayerController.Instance == null) {
            Instantiate(player);
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
