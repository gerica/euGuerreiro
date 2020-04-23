using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumber : MonoBehaviour {
    [SerializeField] Text damageText;
    private float lifeTime = 2f;
    private float moveSpeed = 1f;
    private float placementJitter = 0.5f;

    // Update is called once per frame
    void Update() {
        Destroy(gameObject, lifeTime);
        transform.position += new Vector3(0f, moveSpeed * Time.deltaTime, 0f);
    }

    public void SetDamage(int damageAmount) {
        damageText.text = damageAmount.ToString();
        transform.position += new Vector3(Random.Range(-placementJitter, placementJitter), Random.Range(-placementJitter, placementJitter), 0f);
    }

}
