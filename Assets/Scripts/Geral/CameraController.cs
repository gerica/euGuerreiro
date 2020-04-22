using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour {
    [Header("Configurações")]
    [SerializeField] Tilemap theMap;

    private Transform target;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;
    private float halfWidth;
    private float halfHeight;

    public static CameraController Instance { get; set; }


    // Start is called before the first frame update
    void Start() {
        Instance = this;
        //target = PlayerController.Instance.transform;
        target = FindObjectOfType<PlayerController>().transform;

        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        bottomLeftLimit = theMap.localBounds.min + new Vector3(halfWidth, halfHeight, 0f);
        topRightLimit = theMap.localBounds.max + new Vector3(-halfWidth, -halfHeight, 0f);
        SetBoundsPlayer();
    }

    // Update is called once per frame
    void LateUpdate() {
        Moviment();
        keepCameraInsideBounds();
    }

    private void Moviment() {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    private void keepCameraInsideBounds() {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
            Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y),
            transform.position.z);


    }

    private void SetBoundsPlayer() {
        PlayerController.Instance.SetBounds(theMap.localBounds.min, theMap.localBounds.max);
    }

    IEnumerator WaitForLoad() {
        yield return new WaitForSeconds(1.0f);
    }
}
