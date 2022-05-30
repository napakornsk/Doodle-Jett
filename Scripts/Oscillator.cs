using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 2f;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Oscillation();
    }

    void Oscillation(){
        if (period <= Mathf.Epsilon) { return; }
        // define a lap time per second this mean 2sec per 1 lap
        // cycles continously growing up
        float cycles = Time.time / period; 

        // define a Tau which means around 6 radians per circle
        // constant value of 6.283
        const float tau = Mathf.PI * 2; 

        // going from -1 to 1
        float rawSinWave = Mathf.Sin(cycles * tau); 

        movementFactor = (rawSinWave + 1f) / 2f; // recalculated to go from 0 to 1 so its cleaner

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
    
}
