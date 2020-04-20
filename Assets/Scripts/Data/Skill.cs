using System;

[Serializable]
public class Skill {

    private int id;
    private string name;
    private string type;
    private string difficult;
    private string description;
    private int nivel;

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public string Difficult { get => difficult; set => difficult = value; }
    public string Description { get => description; set => description = value; }
    public int Nivel { get => nivel; set => nivel = value; }
    public string Type { get => type; set => type = value; }

    public int GetNivel() {
        int value = nivel;
        switch (Nivel) {
            case 1:
                value = -1;
                break;
            case 2:
                value = 0;
                break;
            case 3:
                value = 1;
                break;
            case 4:
                value = 2;
                break;
            case 5:
                value = 3;
                break;
            case 6:
                value = 4;
                break;
            case 7:
                value = 5;
                break;
            default:
                value = 6;
                break;
        }
        return value;

    }
}
