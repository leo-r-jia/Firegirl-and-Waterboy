using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private Vector3 finalPosition;
    [SerializeField] private float duration;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private SwitchHandler switchHandler;

    private Vector3 initialPosition, currentPosition;
    private float elapsedTime;

    //Set the object's initial position to where it is on Start
    void Start()
    {
        initialPosition = transform.position;
    }

    //Methods for subscribing to 
    private void OnEnable()
    {
        LeverHandler.ValueChanged += ResetElapsedTime;
    }

    private void OnDisable()
    {
        LeverHandler.ValueChanged += ResetElapsedTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (switchHandler.IsOn() && currentPosition != finalPosition)
        {
            Move(finalPosition);
        } 
        else if (!switchHandler.IsOn() && currentPosition != initialPosition)
        {
            Move(initialPosition);
        }
    }

    //Reset the time taken since movement started
    private void ResetElapsedTime()
    {
        elapsedTime = Time.deltaTime;
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
