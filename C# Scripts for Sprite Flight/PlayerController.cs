
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    public float thrustForce = 1.0f;
    public GameObject boosterFlame;
    public float scoreMultiplier;
    public UIDocument uiDocument;
    public GameObject explosionEffect; //linked with the ExplosionEffect in /assets/prefabs/
    public GameObject gameBorders;
    public InputAction moveForward;
    public InputAction lookDirection;

    private Label scoreText;
    private float score;
    private float elapsedTime;
    private Button restartButton;

    Rigidbody2D rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveForward.Enable();
        lookDirection.Enable();

        rb = GetComponent<Rigidbody2D>();

        boosterFlame.SetActive(false);

        scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel"); //looks for a label with the exxact name "ScoreLabel" (UI version of get component effectively) 
        restartButton = uiDocument.rootVisualElement.Q<Button>("RestartButton");
        restartButton.style.display = DisplayStyle.None;
        restartButton.clicked += RestartGame;

    }

    void UpdateScore()
    {

        elapsedTime += Time.deltaTime;
        Debug.Log("elapsed time: " + elapsedTime);

        score = Mathf.FloorToInt(elapsedTime * scoreMultiplier); //mathf.floortoint will round a float to the nearest whole number (to an int)
        Debug.Log("Score: " + score);

        scoreText.text = "score: " + score;

    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void PlayerMovement()
    {
        if (moveForward.IsPressed())
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(lookDirection.ReadValue<Vector2>()); //converts the screen coords of a mousclick into the world coords 
            //Debug.Log("left button pressed at: " + mousePos );

            Vector2 direction = (mousePos - transform.position).normalized; //calculates the difference between where the mouse was pressed and where the player currently is
            //.normalized will prevent the speed from exceeding the thrustforce (because otherwise holding left click will keep adding force

            transform.up = direction; //changes the y (rotation in 2d) to face this new direction

            rb.AddForce(direction * thrustForce);
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        UpdateScore();

        if (moveForward.WasPressedThisFrame())
        {
            boosterFlame.SetActive(true);
        }
        else if (moveForward.WasReleasedThisFrame())
        {
            boosterFlame.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Destroy(gameBorders);
        Instantiate(explosionEffect, transform.position, transform.rotation); // instantiate effectively "spawns" something in
        restartButton.style.display = DisplayStyle.Flex;
    }
}
