using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InfoPlayer : MonoBehaviour {

    [SerializeField] Text txtNamePlayer;
    [SerializeField] Text txtTimeGame;

    private PlayerData player;
    private string timeGameValue;

    public PlayerData Player { get => player; set => player = value; }
    public string TimeGameValue { get => timeGameValue; set => timeGameValue = value; }

    // Start is called before the first frame update
    void Start() {
        txtNamePlayer.text = player.NamePlayer;
        txtTimeGame.text = TimeGameValue;
    }

    public void Select() {
        //Debug.Log(Session);
        Session.Player = player;
        SceneManager.LoadScene(EnumScenes.Labirinto_1.ToString());
    }

}
