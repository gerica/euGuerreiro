using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class HelpGUI : MonoBehaviour {
    private HelpPoints helpPoints;

    public HelpPoints HelpPoints { get => helpPoints; set => helpPoints = value; }

    protected void CreateItemAdvantageDisadvantage(DisadvantageAdvantage item, GameObject parent, ItemAdvantage prefab) {
        ItemAdvantage itemList = Instantiate(prefab);
        itemList.Id = item.Id;
        itemList.NameValue = item.Name;
        itemList.PointValue = item.PointInit.ToString() + " pontos";
        itemList.DescriptionValue = item.Description;
        itemList.transform.SetParent(parent.transform); // para adicionar no componente pai
        itemList.transform.localScale = new Vector3(1, 1, 0);
    }

    protected void CreateAdvSelected(DisadvantageAdvantage item, GameObject parent, AdvSelected prefab) {
        AdvSelected itemList = Instantiate(prefab);
        itemList.Id = item.Id;
        itemList.NameValue = item.Name;
        itemList.transform.SetParent(parent.transform); // para adicionar no componente pai
        itemList.transform.localScale = new Vector3(1, 1, 0);
    }

    public void UpdatePointToSpend(int value) {
        HelpPoints.UpdatePointToSpend(value);
    }

    public void UpdatePointToSpendComp() {
        HelpPoints.UpdatePointToSpendComp();
    }
}
