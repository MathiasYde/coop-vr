using System;
using UnityEngine;

namespace MessageSystem {
	public abstract class Messenger : ScriptableObject {
		public abstract event Action<Message> onMessage;

		public abstract void SendMessage(Message message);
		public abstract void SendMessage(string topic, string payload);

		public abstract void Listen(string topic);
		public abstract void Update();
		public abstract void Enable();
		public abstract void Disable();
	}
}