using DG.Tweening;
using UnityEngine;

public class Door : MonoBehaviour {
    public void OpenDoor() {
        transform.DOLocalMoveY(-2f, 1f);
    }

    public void CloseDoor() {
        transform.DOLocalMoveY(0f, 1f);
    }
}
