using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private Vector3 finalPosition;
    [SerializeField] private float duration;
    [SerializeField] private AnimationCurve curve;

    private Vector3 initialPosition, currentPosition;
    private float elapsedTime;
    private bool movingToFinal;

    //Set the object's initial position to where it is on Start
    void Start()
    {
        initialPosition = transform.position;
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

        movingToFinal = false;
    }

    //Call this method to send the object to its final position 
    public void MoveToFinal() {
        elapsedTime = Time.deltaTime;

        movingToFinal = true;
    }

    //Move the object from the current position to the destination position smoothly
    private void Move(Vector3 destinationPosition)
    {
        currentPosition = transform.position;
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / duration;

        transform.position = Vector3.Lerp(currentPosition, destinationPosition, curve.Evaluate(percentageComplete));
    }
}
