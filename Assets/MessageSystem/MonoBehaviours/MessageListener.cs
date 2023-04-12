using System;
using UnityEngine;

namespace MessageSystem {
	public class MessageListener : MonoBehaviour {
		[Tooltip("The topic to listen to")]
		[SerializeField] private string topic;

		public MessageUnityEvent onMessageUnityEvent;

		private void onMessage(Message message) {
			onMessageUnityEvent?.Invoke(message);
		}
		
		private void Awake() {
			onMessageUnityEvent ??= new MessageUnityEvent();
		}

		private void OnEnable() {
			MessageManager.instance.Listen(onMessage, topic);
		}

		private void OnDisable() {
			MessageManager.instance.Unlisten(onMessage);
		}
	}
}
