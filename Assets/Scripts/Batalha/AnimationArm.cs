using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationArm : MonoBehaviour {

    public void AutoDestroy() {
        Debug.Log("Chamou");
        Destroy(this.gameObject);
    }

}
