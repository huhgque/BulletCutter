using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwordVisualScript : MonoBehaviour
{
    Animator animator;
    InputManager inputManager;
    bool isLastAttackRight = false;
    bool canAttack = true;
    float attackColdown = 0.5f;
    float attackColdownCounter = 0;
    float waitConnectAttack = 0.15f;
    float waitConnectAttackCounter = 0;
    enum AttackState {
        IDLE,
        ATTACKING,
        WAIT_FOR_CONNECT_ATTACK,
        RECOVER_ATTACK
    }
    AttackState attackState = AttackState.IDLE;
    const string DO_ATTACK = "DoAttack";
    const string RIGHT_ATTACK = "AttackRight";
    const string RECOVER_ATTACK = "RecoverAttack";
    void Awake() {
        animator = GetComponent<Animator>();
        AnimationClip [] animations = animator.runtimeAnimatorController.animationClips;
        attackColdown = animations.First( a => a.name == "SwordAnimation" ).length;
    } 
    void Start() {
        inputManager = InputManager.Instance;
    }
    void Update() {
        switch (attackState) {
            case AttackState.IDLE:
                if(inputManager.IsGetAttack() && canAttack){
                    canAttack = false;
                    animator.SetBool(DO_ATTACK,true);
                    isLastAttackRight = !isLastAttackRight;
                    animator.SetBool(RIGHT_ATTACK,isLastAttackRight);
                    attackState = AttackState.ATTACKING;
                }
                break;
            case AttackState.ATTACKING:
                attackColdownCounter += Time.deltaTime;
                if (attackColdownCounter >= attackColdown){
                    animator.SetBool(DO_ATTACK,false);
                    attackColdownCounter = 0;
                    attackState = AttackState.WAIT_FOR_CONNECT_ATTACK;
                    canAttack = true;
                }
                break;
            case AttackState.WAIT_FOR_CONNECT_ATTACK:
                waitConnectAttackCounter += Time.deltaTime;
                if(inputManager.IsGetAttack() && canAttack){
                    canAttack = false;
                    animator.SetBool(DO_ATTACK,true);
                    isLastAttackRight = !isLastAttackRight;
                    animator.SetBool(RIGHT_ATTACK,true);
                }
                if(waitConnectAttackCounter >= waitConnectAttack){
                    waitConnectAttackCounter = 0;
                    attackState = AttackState.RECOVER_ATTACK;
                    isLastAttackRight = false;
                    animator.SetBool(RECOVER_ATTACK,true);
                }
                break;
            case AttackState.RECOVER_ATTACK:
                attackColdownCounter += Time.deltaTime;
                if (attackColdownCounter >= attackColdown){
                    attackColdownCounter = 0;
                    attackState = AttackState.IDLE;
                    canAttack = true;
                    animator.SetBool(RECOVER_ATTACK,false);
                }
                break;
            default :
                break;
        }
    }
}
