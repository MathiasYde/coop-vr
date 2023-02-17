using UnityEngine;

public class MessageManager : MonoBehaviour {
    public static MessageManager instance;

    [SerializeField] private Messenger messenger;

    public MessageUnityEvent onMessage;

    public void SendMessage(Message message) {
        messenger.SendMessage(message);
    }

    private void Awake() {
        MessageManager.instance ??= this;
        Debug.Assert(messenger != null, "messenger != null", this);
        onMessage ??= new MessageUnityEvent();
    }

    private void Update() {
        messenger?.Update();
    }

    private void OnMessage(Message message) {
        onMessage?.Invoke(message);
    }

    private void OnEnable() {
        messenger.onMessage += OnMessage;
        messenger?.Enable();
    }

    private void OnDisable() {
        messenger.onMessage -= OnMessage;
        messenger?.Disable();
    }
}
