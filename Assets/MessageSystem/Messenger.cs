using System;
using UnityEngine;

public abstract class Messenger : ScriptableObject {
	public abstract event Action<Message> onMessage;
	
	public abstract void SendMessage(Message message);
	public abstract void Update();
	public abstract void Enable();
	public abstract void Disable();
}