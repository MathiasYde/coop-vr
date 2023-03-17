using System;
using System.Collections.Generic;
using UnityEngine;

namespace MessageSystem {
	
	public class MessageManager : MonoBehaviour {
		public static MessageManager instance;

		[SerializeField] private Messenger messenger;

		private Dictionary<string, List<Action<Message>>> listeners;

		public void SendMessage(Message message) { messenger.SendMessage(message); }

		public void Listen(Action<Message> listener, string topic) {
			// TODO: is there a better way to do this?
			if (!listeners.ContainsKey(topic))
				listeners[topic] = new List<Action<Message>>();
			
			listeners[topic].Add(listener);

			messenger.Listen(topic);
		}

		private void Awake() {
			MessageManager.instance ??= this;
			
			Debug.Assert(messenger != null, "messenger is null", this);

			listeners = new Dictionary<string, List<Action<Message>>>();
		}

		private void Update() {
			messenger?.Update();
		}
		
		private void OnMessage(Message message) {
			// call all listeners for message.topic
			if (listeners.TryGetValue(message.topic, out List<Action<Message>> relevantListeners)) {
				foreach (Action<Message> listener in relevantListeners) {
					listener.Invoke(message);
				}
			}
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
}