using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public float randomRangeLower;
    public float randomRangeUpper;

    public float randomSpeedLower;
    public float randomSpeedUpper;

    public float maxSpinSpeed;

    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float randomSize = Random.Range(randomRangeLower, randomRangeUpper);
        transform.localScale = new Vector3(randomSize, randomSize, 1);  //note that the third value serves no purpose as it is a 2d game

        float randomSpeed = Random.Range(randomSpeedLower, randomSpeedUpper) / randomSize;
        Vector2 randomDirection = Random.insideUnitCircle;
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(randomDirection * randomSpeed);

        float randomSpin = Random.Range(-maxSpinSpeed, maxSpinSpeed);
        rb.AddTorque(randomSpin);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
