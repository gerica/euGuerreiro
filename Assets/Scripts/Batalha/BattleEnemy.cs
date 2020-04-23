using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEnemy : MonoBehaviour {
    [SerializeField] BattlePlayer[] enemiesPrefab;
    [SerializeField] string[] enemies;    

    public BattlePlayer[] EnemiesPrefab { get => enemiesPrefab; set => enemiesPrefab = value; }
    public string[] Enemies { get => enemies; set => enemies = value; }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            BattleManager.Instance.BattleStart(this);
        }

    }
}
