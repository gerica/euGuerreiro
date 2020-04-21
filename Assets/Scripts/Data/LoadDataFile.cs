using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadDataFile {
    public static List<DisadvantageAdvantage> listAdvantage = new List<DisadvantageAdvantage>();
    public static List<DisadvantageAdvantage> listDisdvantage = new List<DisadvantageAdvantage>();
    public static List<Skill> listSkills = new List<Skill>();

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

    public static void CreateListSkills() {
        string allText = System.IO.File.ReadAllText(@"./Assets/Scripts/Files/listSkill.txt");
        string[] allTextSplit = allText.Split('\n');
        foreach (var line in allTextSplit) {
            var words = line.Split(';');
            Skill obj = new Skill();
            obj.Id = Int32.Parse(words[0]);
            obj.Name = words[1];
            obj.Type = words[2];
            obj.Difficult = words[3];
            obj.Description = words[4];

            listSkills.Add(obj);
        }
    }

    public static List<HistoryData> LoadHistories() {
        List<HistoryData> datas = new List<HistoryData>();
        HistoryData data;
        string allText = System.IO.File.ReadAllText(@"./Assets/Scripts/Files/history-intro.txt");
        string[] allTextSplit = allText.Split('\n');
        foreach (var line in allTextSplit) {
            data = new HistoryData(line);
            datas.Add(data);
        }

        return datas;
    }
}
