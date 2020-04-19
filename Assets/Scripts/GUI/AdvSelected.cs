using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvSelected : MonoBehaviour {
    [SerializeField] int id;
    [SerializeField] Text name;
    [SerializeField] string nameValue;

    public int Id { get => id; set => id = value; }
    public Text Name { get => name; set => name = value; }
    public string NameValue { get => nameValue; set => nameValue = value; }

    // Start is called before the first frame update
    void Start() {
        Name.text = NameValue;
    }

    public void RemoveAdvantage() {
        MainMenuManager.Instance.RemoveAdvantage(id);
    }
}
