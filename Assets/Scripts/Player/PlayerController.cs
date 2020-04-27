using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Configuração")]
    [SerializeField] Rigidbody2D theRB;
    [SerializeField] Animator myAnimator;
    [SerializeField] float moveSpeed = 10.0f;
    [SerializeField] string areaTransitionName;
    [SerializeField] BattlePlayer battlePlayersPrefabs;


    private PlayerData playerData;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    public string AreaTransitionName { get => areaTransitionName; set => areaTransitionName = value; }
    public static PlayerController Instance { get; set; }
    public bool CanMove { get; set; } = true;
    public PlayerData PlayerData { get => playerData; set => playerData = value; }

    private void Start() {
        // para ter somente um personagem, uma instancia mesmo após trocar de cena
        if (Instance == null) {
            Instance = this;
        } else {
            if (Instance != this) {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update() {
        if (CanMove) {
            Moviment();
        } else {
            theRB.velocity = Vector2.zero;
        }
        keepCameraInsideBounds();
    }

    private void Moviment() {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 moviment = new Vector2(x, y);
        theRB.velocity = moviment * (moveSpeed + Time.deltaTime);
        //theRB.velocity = moviment * moveSpeed;

        myAnimator.SetFloat("moveX", theRB.velocity.x);
        myAnimator.SetFloat("moveY", theRB.velocity.y);

        if (Input.GetAxisRaw("Horizontal") == 1 ||
            Input.GetAxisRaw("Horizontal") == -1 ||
            Input.GetAxisRaw("Vertical") == 1 ||
            Input.GetAxisRaw("Vertical") == -1) {
            if (CanMove) {
                myAnimator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                myAnimator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }
        }
    }

    private void keepCameraInsideBounds() {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
            Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y),
            transform.position.z);
    }

    public void SetBounds(Vector3 botLeft, Vector3 topRight) {
        bottomLeftLimit = botLeft + new Vector3(0.5f, 0.5f, 0f);
        topRightLimit = topRight + new Vector3(-0.5f, -0.5f, 0f);
    }

    internal BattlePlayer GetPlayerBatlePrefab() {
        return battlePlayersPrefabs;

    }
}
