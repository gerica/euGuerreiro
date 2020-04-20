using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData {
    private string namePlayer;
    private int st;
    private int dx;
    private int iq;
    private int ht;
    private string sex;
    private List<int> advantages = new List<int>();
    private List<int> disadvantages = new List<int>();

    public List<int> Advantages { get => advantages; set => advantages = value; }
    public List<int> Disadvantages { get => disadvantages; set => disadvantages = value; }
    public string NamePlayer { get => namePlayer; set => namePlayer = value; }
    public int St { get => st; set => st = value; }
    public int Dx { get => dx; set => dx = value; }
    public int Iq { get => iq; set => iq = value; }
    public int Ht { get => ht; set => ht = value; }
    public string Sex { get => sex; set => sex = value; }

    public void AddAdvantage(int id) {
        advantages.Add(id);
    }

    public void RemoveAdvantage(int id) {
        advantages.Remove(id);
    }

    public void AddDisadvantage(int id) {
        disadvantages.Add(id);
    }

    public void RemoveDisadvantage(int id) {
        disadvantages.Remove(id);
    }

    public override string ToString() {
        return namePlayer + st + dx + iq + ht + sex;
    }
}
