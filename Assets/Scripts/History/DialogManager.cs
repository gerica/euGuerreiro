using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    [SerializeField] Text dialogText;
    [SerializeField] Text nameText;
    [SerializeField] GameObject dialogBox;
    [SerializeField] GameObject nameBox;

    private List<HistoryData> dialogLines;

    private int currentLine;
    private int endLine;
    private bool justStarted;

    public static DialogManager instance;

    public GameObject DialogBox { get => dialogBox; set => dialogBox = value; }


    // Start is called before the first frame update
    void Start() {
        instance = this;
        //dialogText.text = dialogLines[currentLine];
    }

    // Update is called once per frame
    void Update() {
        if (DialogBox.activeInHierarchy) {
            if (Input.GetButtonUp("Fire1")) {
                if (!justStarted) {
                    currentLine++;
                    if (currentLine >= endLine) {
                        DialogBox.SetActive(false);
                        GameManager.Instance.DialogActive = false;

                    } else {
                        CheckIfName();
                        dialogText.text = dialogLines[currentLine].Description;
                        //currentLine++;
                    }
                } else {
                    justStarted = false;
                }
            }
        }
    }

    public void ShowDialog(List<HistoryData> history, int init, int end, bool isPerson) {
        dialogLines = history;
        currentLine = init;
        endLine = end;
        CheckIfName();
        dialogText.text = dialogLines[currentLine].Description;
        DialogBox.SetActive(true);
        justStarted = true;
        nameBox.SetActive(isPerson);
        GameManager.Instance.DialogActive = true;
    }

    public void CheckIfName() {
        if (dialogLines[currentLine].Description.StartsWith("n-player")) {
            nameText.text = dialogLines[currentLine].Description.Replace("n-player", PlayerController.Instance.PlayerData.NamePlayer);
            currentLine++;
        } else if (dialogLines[currentLine].Description.StartsWith("n-")) {
            nameText.text = dialogLines[currentLine].Description.Replace("n-", "");
            currentLine++;
        }
    }

}
