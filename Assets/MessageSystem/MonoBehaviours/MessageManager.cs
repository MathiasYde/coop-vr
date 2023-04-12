using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MessageSystem {
	
	public class MessageManager : MonoBehaviour {
		public static MessageManager instance;

		[SerializeField] private Messenger messenger;

		private Dictionary<string, List<Action<Message>>> listeners;

		public void SendMessage(Message message) { messenger.SendMessage(message); }

		public void Listen(Action<Message> listener, string topic) {
			// TODO: risk of memory leak by loading a new scene and not removing old listeners
			// TODO: is there a better way to do this?
			if (!listeners.ContainsKey(topic))
				listeners[topic] = new List<Action<Message>>();
			
			listeners[topic].Add(listener);

			messenger.Listen(topic);
		}
		
		public void Unlisten(Action<Message> listener) {
			// copilot
			foreach (KeyValuePair<string, List<Action<Message>>> pair in listeners) {
				pair.Value.Remove(listener);
			}
		}

		private void Awake() {
			MessageManager.instance ??= this;
			
			Debug.Assert(messenger != null, "messenger is null", this);

			listeners = new Dictionary<string, List<Action<Message>>>();
		}

		private void Update() {
			messenger.Update();
		}
		
		private void OnMessage(Message message) {
			// call all listeners for message.topic
			if (listeners.TryGetValue(message.topic, out List<Action<Message>> relevantListeners)) {
				foreach (Action<Message> listener in relevantListeners) {
					listener.Invoke(message);
				}
			}
		}
		
		private void OnSceneLoad(Scene scene, LoadSceneMode mode) {
			// remove all listeners
			listeners?.Clear();
		}

		private void OnEnable() {
			messenger.onMessage += OnMessage;
			messenger.Enable();

			SceneManager.sceneLoaded += OnSceneLoad;
		}

		private void OnDisable() {
			messenger.onMessage -= OnMessage;
			messenger.Disable();
			
			SceneManager.sceneLoaded -= OnSceneLoad;
		}
	}
}