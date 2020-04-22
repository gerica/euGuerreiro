using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HistoryGame : MonoBehaviour {
    [SerializeField] string nameChapter;

    public static HistoryGame Instance { get; set; }
    private List<HistoryData> datas;
    private int count = 0;

    // Start is called before the first frame update
    void Start() {
        Instance = this;
        datas = LoadDataFile.LoadHistories(nameChapter);
    }

}
