using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyFactory {
    public static PlayerData crateEnemy(string name) {
        if (EnumEnemies.Skeleto1.ToString() == name) {
            return new Skeleto1();
        }
        return null;
    }
}
