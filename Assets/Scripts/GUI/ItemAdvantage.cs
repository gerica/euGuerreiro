using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemAdvantage : MonoBehaviour {
    [SerializeField] int id;
    [SerializeField] Text name;
    [SerializeField] string nameValue;
    [SerializeField] Text point;
    [SerializeField] string pointValue;
    [SerializeField] Text description;
    [SerializeField] string descriptionValue;
    //[SerializeField] Button addButton;

    public Text Name { get => name; set => name = value; }
    public Text Point { get => point; set => point = value; }
    public Text Description { get => description; set => description = value; }
    public string NameValue { get => nameValue; set => nameValue = value; }
    public string PointValue { get => pointValue; set => pointValue = value; }
    public string DescriptionValue { get => descriptionValue; set => descriptionValue = value; }
    public int Id { get => id; set => id = value; }

    // Start is called before the first frame update
    void Start() {
        name.text = NameValue;
        if (point) {
            point.text = PointValue;
        }
        if (description) {
            description.text = DescriptionValue;
        }
    }

    public void AddAdvantage() {
        MainMenuManager.Instance.AddAdvantage(id);
    }
    public void AddDisadvantage() {
        MainMenuManager.Instance.AddDisdvantage(id);
    }
}
