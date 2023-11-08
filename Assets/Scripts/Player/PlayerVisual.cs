using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    Player player;
    void Start() {
        player = Player.Instance;
    }

    void Update()
    {
        FollowCursor();   
    }
    void FollowCursor(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool hit = Physics.Raycast(
            ray : ray,
            hitInfo : out RaycastHit hitInfo,
            layerMask : EasyLayerMask.I.HitOnly("Ground").Value,
            maxDistance : float.MaxValue
        );
        if (hit) {
            Vector3 faceToWard = new Vector3(hitInfo.point.x,transform.forward.y,hitInfo.point.z);
            player.transform.forward =  faceToWard - player.transform.position ;
        }
    }
}
