﻿using System;
using System.Collections.Generic;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Valve.Newtonsoft.Json;

namespace MessageSystem {
	[CreateAssetMenu(menuName = "MessageManager/Messengers/MQTTMessenger")]
	public class MQTTMessenger : Messenger {
		[Header("Broker configuration")]
		[SerializeField] private string host = "localhost";
		[SerializeField] private int port = 1883;
		
		[Header("Connection configuration")]
		[SerializeField] private bool autoConnect = true;
	
		public override event Action<Message> onMessage;

		private MqttClient client;

		public override void SendMessage(string topic, string payload) {
			client.Publish(topic, System.Text.Encoding.UTF8.GetBytes(payload));
		}
	
		public override void SendMessage(Message message) {
			string json = JsonConvert.SerializeObject(message.payload);
			client.Publish(message.topic, System.Text.Encoding.UTF8.GetBytes(json));
		}

		private void onMqttMessage(object sender, MqttMsgPublishEventArgs args) {
			// MainThreadDispatcher is used since the MqttClient uses threading
			MainThreadDispatcher.Enqueue(() => {
				// parse the message and call onMessage event
				try {
					string json = System.Text.Encoding.UTF8.GetString(args.Message);
					var payload = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
					
					onMessage.Invoke(new Message(args.Topic, payload));
				} catch (Exception exception) {
					Debug.LogError(exception);
				}
			});
		}

		public override void Update() { }

		public override void Listen(string topic) {
			client.Subscribe(new[] { topic }, new []{ MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
			Debug.Log($"MQTTMessenger: Subscribing to {topic}", this);
		}

		public override void Enable() {
			try {
				client = new MqttClient(host, port, false, null, null, MqttSslProtocols.None);

				string clientId = Guid.NewGuid().ToString();
				if (autoConnect)
					client.Connect(clientId, null, null);
				
				if (client.IsConnected)
					Debug.Log($"MQTTMessenger: Connected to MQTT broker as {clientId}");
			}
			catch (Exception exception) {
				Debug.LogError(exception, this);
			}

			client.MqttMsgPublishReceived += onMqttMessage;
		}

		public override void Disable() {
			client.MqttMsgPublishReceived -= onMqttMessage;
			client = null;
		}
	}
}