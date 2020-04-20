using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadDataFile {
    public static List<DisadvantageAdvantage> listAdvantage = new List<DisadvantageAdvantage>();
    public static List<DisadvantageAdvantage> listDisdvantage = new List<DisadvantageAdvantage>();

    public static void CreateListAdvantage() {
        string allText = System.IO.File.ReadAllText(@"./Assets/Scripts/Files/listAdvantage.txt");
        string[] allTextSplit = allText.Split('\n');
        foreach (var line in allTextSplit) {
            var words = line.Split(';');
            DisadvantageAdvantage adv = new DisadvantageAdvantage();
            adv.Id = Int32.Parse(words[0]);
            adv.Name = words[1];
            adv.PointInit = Int32.Parse(words[2]);
            adv.PointContinue = Int32.Parse(words[3]);
            adv.Description = words[4];

            listAdvantage.Add(adv);
        }
    }

    public static void CreateListDisadvantage() {
        string allText = System.IO.File.ReadAllText(@"./Assets/Scripts/Files/listDisadvantage.txt");
        string[] allTextSplit = allText.Split('\n');
        foreach (var line in allTextSplit) {
            var words = line.Split(';');
            DisadvantageAdvantage adv = new DisadvantageAdvantage();
            adv.Id = Int32.Parse(words[0]);
            adv.Name = words[1];
            adv.PointInit = Int32.Parse(words[2]);
            adv.PointContinue = Int32.Parse(words[3]);
            adv.Description = words[4];

            listDisdvantage.Add(adv);
        }
    }
}
