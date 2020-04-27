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
    private int htPlayer;
    private float speedBasic;
    private EnumSex sex;
    private List<DisadvantageAdvantage> advantages = new List<DisadvantageAdvantage>();
    private List<DisadvantageAdvantage> disadvantages = new List<DisadvantageAdvantage>();
    private List<Skill> skills = new List<Skill>();
    private int historyProgress;
    private bool isEnemy;
    //Esquiva: O valor da defesa Esquiva do personagem(v.Esquiva, pág. 374) é igual à sua Velocidade Básica +3, ig­ norando as frações.Por exemplo, um personagem com uma Velocidade Bá­ sica de 5,25 terá uma Esquiva de 8. 
    private int dodge;


    public string NamePlayer { get => namePlayer; set => namePlayer = value; }
    public int St { get => st; set => st = value; }
    public int Dx { get => dx; set => dx = value; }
    public int Iq { get => iq; set => iq = value; }
    public int Ht { get => ht; set => ht = value; }
    public List<DisadvantageAdvantage> Advantages { get => advantages; set => advantages = value; }
    public List<DisadvantageAdvantage> Disadvantages { get => disadvantages; set => disadvantages = value; }
    public List<Skill> Skills { get => skills; set => skills = value; }
    public EnumSex Sex { get => sex; set => sex = value; }
    public int HistoryProgress { get => historyProgress; set => historyProgress = value; }
    public float SpeedBasic {
        get {
            // Para calcular a Velocidade Básica de um personagem, some seus valores de HT e DX e divida o resultado por 4.Não ar­
            //redonde esse valor. 5,25 é melhor que 5!
            return (ht + dx) / 4;
        }
        set => speedBasic = value;
    }

    public bool IsEnemy { get => isEnemy; set => isEnemy = value; }
    public int HtPlayer { get => htPlayer; set => htPlayer = value; }
    public int Dodge {
        get {
            return Convert.ToInt32(SpeedBasic + 3);
        }
        set => dodge = value;
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

    internal void LoseHT(int damage) {
        HtPlayer -= damage;
    }
       
}
