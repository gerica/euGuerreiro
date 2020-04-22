using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAcivator : MonoBehaviour {
    [SerializeField] string pathChapter;
    [SerializeField] int pathInit;
    [SerializeField] int pathEnd;
    private List<HistoryData> list;
    //private string[] lines;
    private bool canActivate;

    public bool isPerson = true;
    // Start is called before the first frame update
    void Start() {
        list = LoadDataFile.LoadHistories(pathChapter);
    }

    // Update is called once per frame
    void Update() {
        if (canActivate && Input.GetButtonDown("Fire1") && !DialogManager.instance.DialogBox.activeInHierarchy) {
            //lines = new string[(pathEnd + 1) - pathInit];
            //for (int x = pathInit; x <= pathEnd; x++) {
            //    Debug.Log(list[x].Description);

            //    lines[x] = list[x].Description;
            //}
            //Debug.Log(lines);
            DialogManager.instance.ShowDialog(list, pathInit, pathEnd, isPerson);
            //GameManager.Instance.
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            canActivate = false;
        }
    }

}
