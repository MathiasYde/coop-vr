using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MessageManager/Messengers/NetworkMessenger")]
public class NetworkMessenger : Messenger {
	[Header("Network configuration")]
	[SerializeField] private string ssid;
	[SerializeField] private string password;
	[SerializeField] private string port;
	
	public override event Action<Message> onMessage;
	
	public override void SendMessage(Message message) {
		Debug.Log("NetworkMessenger: Sending message...");
	}

	public override void Update() { }
	public override void Enable() { }
	public override void Disable() { }
}