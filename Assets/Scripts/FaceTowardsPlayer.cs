using UnityEngine;
using Valve.VR.InteractionSystem;

public class FaceTowardsPlayer : MonoBehaviour {
    void Update() {
        Transform target = Player.instance.hmdTransform;
        
        transform.LookAt(target);
    }
}
