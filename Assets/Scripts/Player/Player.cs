using System.Collections;
using System.Collections.Generic;

public class Player {
    private List<int> advantages = new List<int>();
    private List<int> disadvantages = new List<int>();

    public List<int> Advantages { get => advantages; set => advantages = value; }
    public List<int> Disadvantages { get => disadvantages; set => disadvantages = value; }

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
}
