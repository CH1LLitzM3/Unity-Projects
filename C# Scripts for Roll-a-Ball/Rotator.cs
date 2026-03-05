using UnityEngine;

public class Rotator : MonoBehaviour
{

    public float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0.5f, 1, 1.5f) * rotationSpeed);
    }
}
