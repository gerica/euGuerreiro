using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HistoryGame : MonoBehaviour {
    [SerializeField] Text infoTitle;
    [SerializeField] Text infoDescription;

    private List<HistoryData> datas;
    private int count = 0;

    // Start is called before the first frame update
    void Start() {
        datas = LoadDataFile.LoadHistories();
        infoTitle.text = datas[count++].Description;
        infoDescription.text = datas[count].Description;
    }

    public void Next() {
        count++;
        if (count >= datas.Count) {
            SceneManager.LoadScene(EnumScenes.Labirinto_1.ToString());
        } else {
            infoDescription.text = datas[count].Description;
        }
    }
}
