using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class KeyPressed : MonoBehaviour
{
    public UnityEvent WKey, AKey, DKey, UpArrowKey, LeftArrowKey, RightArrowKey;
    
    //Returns true if this script is executing under Firegirl
    bool IsPlayerOne()
    {
        return gameObject.name == "Player 1";
    }

    //Invoke an event depending on which horizontal movement key the player pressed
    public void MoveKeyPressed(InputAction.CallbackContext context)
    {
        float dirX = context.ReadValue<Vector2>().x;

        if (IsPlayerOne())
        {
            if (dirX < 0)
                AKey.Invoke();
            else if (dirX > 0)
                DKey.Invoke();
        }
        else
        {
            if (dirX < 0)
                LeftArrowKey.Invoke();
            else if (dirX > 0)
                RightArrowKey.Invoke();
        }
    }

    //Invoke an event depending on which player jumped
    public void JumpKeyPressed(InputAction.CallbackContext context)
    {
        if (IsPlayerOne())
            WKey.Invoke();
        else
            UpArrowKey.Invoke();
    }
}
