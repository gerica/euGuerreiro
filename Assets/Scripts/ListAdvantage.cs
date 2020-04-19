using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListAdvantage {
    public static List<Advantage> list = new List<Advantage>();

    public static void CreateList() {
        string allText = System.IO.File.ReadAllText(@"./Assets/Scripts/listAdvantage.txt");
        string[] allTextSplit = allText.Split('\n');
        foreach (var line in allTextSplit) {
            var words = line.Split(';');
            Advantage adv = new Advantage();
            adv.Id = Int32.Parse(words[0]);
            adv.Name = words[1];
            adv.PointInit = Int32.Parse(words[2]);
            adv.PointContinue = Int32.Parse(words[3]);
            adv.Description = words[4];

            list.Add(adv);
        }
    }
}
