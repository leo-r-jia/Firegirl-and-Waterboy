using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private Vector2 finalPosition;
    [SerializeField] private float duration;
    [SerializeField] private AnimationCurve curve;

    private Vector2 initialPosition, currentPosition, calledPosition;
    private float elapsedTime, scaledDuration;
    private bool movingToFinal;

    //Set the object's initial position to where it is on Start and scaledDuration to duration
    void Start()
    {
        initialPosition = transform.position;
        scaledDuration = duration;
    }

    //Move the object to its destination if it isn't already there
    public void Update()
    {
        currentPosition = transform.position;

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
        //Called position is where the object was when this was called
        calledPosition = currentPosition;

        //Scaled duration is the duration scaled to how far the object is from its desitination
        scaledDuration = duration - duration * (1 - InverseLerp(initialPosition, finalPosition, calledPosition));

        movingToFinal = false;
    }

    //Call this method to send the object to its final position 
    public void MoveToFinal() 
    {
        elapsedTime = Time.deltaTime;
        //Called position is where the object was when this was called
        calledPosition = currentPosition;

        //Scaled duration is the duration scaled to how far it is from its desitination
        scaledDuration = duration - duration * InverseLerp(initialPosition, finalPosition, calledPosition);

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
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / scaledDuration;
        Debug.Log(elapsedTime + " / " + scaledDuration + " = " + percentageComplete);

        transform.position = Vector2.Lerp(calledPosition, destinationPosition, curve.Evaluate(percentageComplete));
    }
}
