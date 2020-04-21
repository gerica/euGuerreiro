using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryController {
    private List<HistoryData> histories = new List<HistoryData>();

    public List<HistoryData> Histories { get => histories; set => histories = value; }
}
