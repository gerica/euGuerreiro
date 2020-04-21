using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryData {
    private string description;

    public HistoryData(string description) {
        this.description = description;
    }

    public string Description { get => description; set => description = value; }
}
