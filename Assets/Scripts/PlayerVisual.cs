using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool hit = Physics.Raycast(
            ray : ray,
            hitInfo : out RaycastHit hitInfo,
            layerMask : EasyLayerMask.I.HitOnly("Ground").Value,
            maxDistance : 100
        );
        if (hit) {
            transform.forward = hitInfo.point;
        }
    }
}
