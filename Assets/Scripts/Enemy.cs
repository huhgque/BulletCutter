using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int hp;
    [SerializeField] float iFrameTime;
    float iFrameCounter = 0;
    bool canBeHit = true;
    [SerializeField] float trackDistant = 5;
    [SerializeField] float rotateSpeed = 1f;
    [SerializeField] float maxMagnitudelta = 1f;
    [SerializeField] float shotAngle = 0;
    [SerializeField] GameObject bulletSpawnPosition;
    /// <summary>
    /// Bullet per second
    /// </summary>
    [SerializeField] float fireRate = 1f;
    [SerializeField] EnemyProjectile projectile;
    float coldownTimer;
    float fireColdown = 0;
    bool canShot = true;
    Player player;
    void Start() {
        player = Player.Instance;
        coldownTimer = 1/fireRate;
    }
    // Update is called once per frame
    void Update()
    {
        if(!player) return;
        Vector3 playerLocation = player.transform.position;
        if (IsPlayerInrange()){
            Vector3 towardPlayer =  playerLocation - transform.position ;
            transform.forward = Vector3.RotateTowards(transform.forward, new Vector3(towardPlayer.x,transform.forward.y,towardPlayer.z) ,Time.deltaTime * rotateSpeed , maxMagnitudelta);
        }
        ShotPlayer();
    }
    bool IsPlayerInrange(){
        float distant = Vector3.Distance(player.transform.position,transform.position) ;
        if (distant > trackDistant) return false;
        EasyLayerMask easyLayerMask = new();
        easyLayerMask.HitOnly("Player").And("Wall");
        bool hitPlayer = Physics.Raycast(
            origin:  transform.position,
            direction: (player.transform.position - transform.position).normalized,
            maxDistance : trackDistant,
            layerMask : easyLayerMask.Value,
            hitInfo : out RaycastHit hitInfo );
        if(hitPlayer){
            return hitInfo.transform.TryGetComponent<Player>(out Player hitP);
        }else{
            return false;
        }
    }
    void ShotPlayer(){
        fireColdown += Time.deltaTime;
        if (fireColdown >= coldownTimer) canShot = true;
        if (IsPlayerInrange() && canShot && IsInShotingAngle()){
            EnemyProjectile bullet = Instantiate(projectile);
            bullet.origin = transform.position;
            bullet.gameObject.transform.position = bulletSpawnPosition.transform.position;
            bullet.gameObject.transform.rotation = transform.rotation;
            bullet.gameObject.SetActive(true);
            canShot = false;
            fireColdown = 0;
        }
    }
    bool IsInShotingAngle(){
        float angle = Vector3.Angle(transform.forward, player.transform.position - transform.position );
        return angle <= shotAngle;
    }
}
