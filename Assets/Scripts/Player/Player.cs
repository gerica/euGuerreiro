using System.Collections;
using System.Collections.Generic;

public class Player {
    private List<int> advantages = new List<int>();

    public List<int> Advantages { get => advantages; set => advantages = value; }

    public void AddAdvantage(int id) {
        advantages.Add(id);
    }

    public void RemoveAdvantage(int id) {
        advantages.Remove(id);
    }
}
