using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    [SerializeField] SwordVisualScript swordVisualScript;
    bool invokeParray = false;
    [SerializeField] float parryRadius = 2.5f;
    void Start() {
        swordVisualScript.OnAttackAnimationStart += ActivateCollider;
        swordVisualScript.OnAttackAnimationEnd += DeactivateCollider;
    }
    void Update() {
        if (invokeParray){
            RaycastHit[] hits = Physics.SphereCastAll(
                origin : transform.position,
                radius : parryRadius,
                direction : transform.forward,
                layerMask : EasyLayerMask.I.HitOnly("EnemyProjectile").Value,
                maxDistance : 0);
            Debug.DrawRay(transform.position,transform.forward);
            if (hits.Length > 0){
                foreach(RaycastHit hit in hits){
                    EnemyProjectile enemyProjectile = hit.transform.GetComponent<EnemyProjectile>();
                    if (!enemyProjectile.IsParried){
                        enemyProjectile.transform.forward = transform.forward;
                        enemyProjectile.IsParried = true;
                    }
                }
            }
        }
    }
    void ActivateCollider(object sender,EventArgs args){
        invokeParray = true;
    }
    void DeactivateCollider(object sender,EventArgs args){
        invokeParray = false;
    }

    void OnCollisionEnter(Collision other) {
        Debug.Log("Epic");    
    }
    void OnCollisionStay(Collision other) {
        Debug.Log("EPic");
    }
}
