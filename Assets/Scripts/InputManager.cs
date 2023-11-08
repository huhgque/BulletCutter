using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance{get;private set;}
    private PlayerInput playerInput;
    public bool JustAttack {get;private set;} = false;
    private void Awake() {
        playerInput = new();
        playerInput.GamePlay.Enable();
        Instance = this;
    }
    void Update() {
        if (IsGetAttack() && !JustAttack){
            JustAttack = true;
        }else{
            JustAttack = false;
        }
    }
    public Vector2 GetMovementDirection(){
        return playerInput.GamePlay.Move.ReadValue<Vector2>();
    }

    public bool IsGetAttack(){
        return playerInput.GamePlay.Attack.IsPressed();
    }
}
