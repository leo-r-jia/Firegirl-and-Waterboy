using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private Vector2 finalPosition;
    [SerializeField] private float duration;
    [SerializeField] private AnimationCurve curve;

    private Vector2 initialPosition, currentPosition;
    private float elapsedTime, scaledDuration;
    private bool movingToFinal;

    //Set the object's initial position to where it is on Start
    void Start()
    {
        initialPosition = transform.position;
        scaledDuration = duration;
    }

    //Move the object to its destination if it isn't already there
    public void Update()
    {
        if (movingToFinal && currentPosition != finalPosition)
        {
            Move(finalPosition);
        } 
        else if (!movingToFinal && currentPosition != initialPosition)
        {
            Move(initialPosition);
        }
    }

    //Call this method to send the object back to its starting position 
    public void MoveToInitial()
    {
        elapsedTime = Time.deltaTime;

        scaledDuration = duration - duration * (1 - InverseLerp(initialPosition, finalPosition, currentPosition));

        movingToFinal = false;
    }

    //Call this method to send the object to its final position 
    public void MoveToFinal() 
    {
        elapsedTime = Time.deltaTime;
        
        scaledDuration = duration - duration * InverseLerp(initialPosition, finalPosition, currentPosition);

        movingToFinal = true;
    }

    //Custom InverseLerp function for dealing with vectors. Returns the percentage of how far the currentPoint is from start to end
    public static float InverseLerp(Vector2 a, Vector2 b, Vector2 value)
    {
        Vector2 AB = b - a;
        Vector2 AV = value - a;
        return Vector2.Dot(AV, AB) / Vector2.Dot(AB, AB);
    }

    //Move the object from the current position to the destination position smoothly
    private void Move(Vector3 destinationPosition)
    {
        currentPosition = transform.position;
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / scaledDuration;

        transform.position = Vector3.Lerp(currentPosition, destinationPosition, curve.Evaluate(percentageComplete));
    }
}
