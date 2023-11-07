using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] int damage;
    public Vector3 origin {set; private get;} 
    float outOfBoundDistant = 100;
    // Update is called once per frame
    void Update()
    {

        EasyLayerMask layerMask = new();
        layerMask.HitOnly("Player").And("Wall"); 
        RaycastHit raycastHit; 
        bool didHit = Physics.Raycast(
            origin : transform.position,
            direction : transform.forward,
            maxDistance : Time.deltaTime * moveSpeed , 
            layerMask: layerMask.Value,
            hitInfo : out raycastHit);
        if (didHit){
            Player player = raycastHit.transform?.GetComponent<Player>();
            if (player) player.TakeDamage(damage); 
            Destroy(gameObject);
        }
        MovementUpdate();
        CheckOutOfBound();
    }
    void MovementUpdate(){
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }
    void CheckOutOfBound(){
        if(Vector3.Distance(transform.position,origin) > outOfBoundDistant) Destroy(gameObject);
    }
}
