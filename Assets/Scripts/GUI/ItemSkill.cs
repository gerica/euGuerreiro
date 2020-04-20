using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSkill : MonoBehaviour {
    [SerializeField] int id;
    [SerializeField] Text nameSkill;
    private string nameValue;
    [SerializeField] Text difficult;
    private string difficultValue;
    [SerializeField] Text nivel;
    private string nivelValue;
    [SerializeField] Text points;
    private string pointsValue = "0 ponto";
    [SerializeField] Text description;
    private string descriptionValue;
    private int count;
    private int totalCost;

    public int Id { get => id; set => id = value; }
    public Text NameSkill { get => nameSkill; set => nameSkill = value; }
    public string NameValue { get => nameValue; set => nameValue = value; }
    public Text Difficult { get => difficult; set => difficult = value; }
    public string DifficultValue { get => difficultValue; set => difficultValue = value; }
    public Text Description { get => description; set => description = value; }
    public string DescriptionValue { get => descriptionValue; set => descriptionValue = value; }
    public Text Points { get => points; set => points = value; }
    public string PointsValue { get => pointsValue; set => pointsValue = value; }
    public Text Nivel { get => nivel; set => nivel = value; }
    public string NivelValue { get => nivelValue; set => nivelValue = value; }
    public int TotalCost { get => totalCost; set => totalCost = value; }
    public int Count { get => count; set => count = value; }
    

    //[SerializeField] Button addButton;


    // Start is called before the first frame update
    void Start() {
        NameSkill.text = NameValue;
        Difficult.text = DifficultValue;
        Description.text = DescriptionValue;
        UpdatePoints();
    }

    private void UpdatePoints() {
        Points.text = PointsValue;
        Nivel.text = Count + " Nível";
    }

    public void Add() {
        Count++;
        CalculeteCost();

    }

    public void Remove() {
        if (Count > 0) {
            Count--;
            CalculeteCost();
        }
    }

    public void CalculeteCost() {

        switch (Count) {
            case 0:
                totalCost = Count;
                pointsValue = totalCost.ToString() + " ponto";                
                break;
            case 1:
                totalCost = Count;
                pointsValue = totalCost.ToString() + " ponto";
                break;
            case 2:
                totalCost = Count;
                pointsValue = totalCost.ToString() + " pontos";
                break;
            case 3:
                totalCost = Count + 1;
                pointsValue = totalCost.ToString() + " pontos";
                break;
            case 4:
                totalCost = Count + 4;
                pointsValue = totalCost.ToString() + " pontos";
                break;
            case 5:
                totalCost = Count + 7;
                pointsValue = totalCost.ToString() + " pontos";
                break;
            case 6:
                totalCost = Count + 10;
                pointsValue = totalCost.ToString() + " pontos";
                break;
            case 7:
                totalCost = Count + 13;
                pointsValue = totalCost.ToString() + " pontos";
                break;
            default:
                totalCost = (Count * 4) + 10;
                pointsValue = totalCost.ToString() + " pontos";
                break;
        }

        UpdatePoints();
        MainMenuManager.Instance.UpdatePoints();

    }

}
