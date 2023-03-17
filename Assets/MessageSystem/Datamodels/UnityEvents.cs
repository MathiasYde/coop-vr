using System;
using UnityEngine.Events;

namespace MessageSystem {
	[Serializable]
	public class MessageUnityEvent : UnityEvent<Message> {}
}