﻿using System;
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
    private List<Skill> skills = new List<Skill>();

    public string NamePlayer { get => namePlayer; set => namePlayer = value; }
    public int St { get => st; set => st = value; }
    public int Dx { get => dx; set => dx = value; }
    public int Iq { get => iq; set => iq = value; }
    public int Ht { get => ht; set => ht = value; }
    public string Sex { get => sex; set => sex = value; }
    public List<int> Advantages { get => advantages; set => advantages = value; }
    public List<int> Disadvantages { get => disadvantages; set => disadvantages = value; }
    public List<Skill> Skills { get => skills; set => skills = value; }

    public void AddAdvantage(int id) {
        Advantages.Add(id);
    }

    public void RemoveAdvantage(int id) {
        Advantages.Remove(id);
    }

    public void AddDisadvantage(int id) {
        Disadvantages.Add(id);
    }

    public void RemoveDisadvantage(int id) {
        Disadvantages.Remove(id);
    }

    public override string ToString() {
        return NamePlayer + St + Dx + Iq + Ht + Sex;
    }

    public int GetNivelSkill(Skill skill) {
        int value = 3;

        switch (skill.Type) {
            case "ST":
                value = st + skill.GetNivel();
                break;
            case "DX":
                value = dx + skill.GetNivel();
                break;
            case "IQ":
                value = iq + skill.GetNivel();
                break;
            case "HT":
                value = ht + skill.GetNivel();
                break;
            default:
                break;
        }
        return value;
    }
}