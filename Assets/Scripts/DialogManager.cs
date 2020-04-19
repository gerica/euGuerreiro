using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    [SerializeField] Text dialogText;
    [SerializeField] Text nameText;
    [SerializeField] GameObject dialogBox;
    [SerializeField] GameObject nameBox;

    [SerializeField] string[] dialogLines;

    [SerializeField] int currentLine;
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
                    if (currentLine >= dialogLines.Length) {
                        DialogBox.SetActive(false);
                        PlayerController.Instance.CanMove = true;
                    } else {
                        CheckIfName();
                        dialogText.text = dialogLines[currentLine];
                        //currentLine++;
                    }
                } else {
                    justStarted = false;
                }
            }
        }
    }

    public void ShowDialog(string[] newLines, bool isPerson) {
        dialogLines = newLines;
        currentLine = 0;
        CheckIfName();
        dialogText.text = dialogLines[currentLine];
        DialogBox.SetActive(true);
        justStarted = true;
        nameBox.SetActive(isPerson);
        PlayerController.Instance.CanMove = false;
    }

    public void CheckIfName() {
        if (dialogLines[currentLine].StartsWith("n-")) {
            nameText.text = dialogLines[currentLine].Replace("n-", "");
            currentLine++;
        }
    }

}
