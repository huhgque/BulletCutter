using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int hp;
    [SerializeField] float iFrameTime;
    float iFrameCounter = 0;
    bool canBeHit = true;
    [SerializeField] float moveSpeed;
    public static Player Instance {get; set;}
    InputManager inputManager;
    // Start is called before the first frame update
    float raycastDistant = 1f;
    float playerRadius = 0.5f;
    void Awake() {
        Instance = this;
    }
    void Start()
    {
        inputManager = InputManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new(
                x:inputManager.GetMovementDirection().x,
                y:0,
                z:inputManager.GetMovementDirection().y);
        direction.Normalize();
        RaycastHit hit;
        bool isHit = Physics.CapsuleCast(
            point1 : transform.position,
            point2 : transform.position + Vector3.up,
            radius : playerRadius, 
            direction : direction ,
            maxDistance : Time.deltaTime * moveSpeed ,
            layerMask  : EasyLayerMask.I.HitOnly("Wall").Value,
            hitInfo : out hit
            );
        if (isHit) return;
        if (inputManager.GetMovementDirection() != Vector2.zero){
            transform.position += direction * Time.deltaTime * moveSpeed;
        }
    }
    public void TakeDamage(int damage){
        Debug.Log(damage);
    }
}
