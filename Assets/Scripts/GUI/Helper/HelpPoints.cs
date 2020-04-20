using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpPoints {
    private int pointsToSpend = 100;
    private int pointsToSpendSkill = 0;
    private Text pointsToSpendComp;

    public int PointsToSpend { get => pointsToSpend; set => pointsToSpend = value; }
    public Text PointsToSpendComp { get => pointsToSpendComp; set => pointsToSpendComp = value; }
    public int PointsToSpendSkill { get => pointsToSpendSkill; set => pointsToSpendSkill = value; }

    public void UpdatePointToSpend(int value) {
        PointsToSpend += value;
        UpdatePointToSpendComp();
    }

    public void UpdatePointToSpendComp() {
        pointsToSpendComp.text = (pointsToSpend - pointsToSpendSkill).ToString();
    }
}
