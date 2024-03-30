using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerController : MonoBehaviour
{

    [SerializeField] private float playerSpeed = 2.0f;

    protected CharacterController controller;
    protected PlayerActionsExample playerInput;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    public TextMeshProUGUI ScoreField;
    // 0-32: R
    // 33-65: G
    // 66-98: B
    // not 0, 1, 2 since it cannot provide sufficient randomness smh
    private int PlayerColor = 0;
    // texture for player color
    public Material Player1;
    public Material Player2;
    public Material Player3;
    // score
    public static int score = 0;
    // spawn timer
    private float nextSpawn = 0f;

    public GameOver GameOverScreen; 
    public GameObject pickupPrefab;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = new PlayerActionsExample();
    }

    private void Start()
    {
        GameStart();
    }

    private void PickupSpawn()
    {
        // Random position within the arena (-450, -450 to 450, 450)
        GameObject go = (GameObject)Instantiate(pickupPrefab, new Vector3(Random.Range(-450, 450), 15, Random.Range(-450, 450)), Quaternion.identity);
        go.tag = "Pickup";
        go.GetComponent<PickupController>().SetColor(Random.Range(1, 100));
    }

    private void GameStart()
    {
        // New score
        score = 0;
        // Instantiate some pickups, say, 10
        for (int i = 0; i < 10; i++)
        {
            PickupSpawn();
        }
    }

    private void Update()
    {
        Vector2 movement = playerInput.Player.Move.ReadValue<Vector2>();
        Debug.Log(movement);
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        if (Time.time >= nextSpawn && nextSpawn > 0)
        {
            Debug.Log("Here you go");
            PickupSpawn();
            // Reset the value to prevent spawning indefinitely
            nextSpawn = 0f;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Pickup"))
        {
            // Game over condition
            // If colors are different...
            if (PlayerColor / 33 != col.gameObject.GetComponent<PickupController>().GetColor() / 33)
                GameOver();
            else
            {
                nextSpawn = Time.time + 1f;
                Debug.Log(Time.time);
                Debug.Log(nextSpawn);
                col.gameObject.SetActive(false);
                PlayerColor = Random.Range(1, 100);
                // Change player color according to this
                if (PlayerColor / 33 == 0)
                {
                    GetComponent<MeshRenderer>().material = Player1;
                }
                else if (PlayerColor / 33 == 1)
                {
                    GetComponent<MeshRenderer>().material = Player2;
                }
                else
                {
                    GetComponent<MeshRenderer>().material = Player3;
                }
                score++;
                // Update the score counter as well
                ScoreField.text = $"Score: {score}";
                // If score becomes 10 you win
                if (score == 10)
                    GameOver();
            }
        }
    }

    void GameOver()
    {
        GameOverScreen.Init(score);
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

}
