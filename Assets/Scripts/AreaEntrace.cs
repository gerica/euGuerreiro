using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrace : MonoBehaviour {
    [SerializeField] string trancitionName;

    public string TrancitionName { get => trancitionName; set => trancitionName = value; }

    // Start is called before the first frame update
    void Start() {
        if (PlayerController.Instance != null && TrancitionName == PlayerController.Instance.AreaTransitionName) {
            PlayerController.Instance.transform.position = transform.position;
            //Debug.Log(PlayerController.instance.transform.position);
        }

        UiFade.instance.FadeFromBlack();
    }

    // Update is called once per frame
    void Update() {

    }
}
