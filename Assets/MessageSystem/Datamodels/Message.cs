using System;
using System.Collections.Generic;
using UnityEngine;

namespace MessageSystem {
	public class Message {
		public readonly string topic;
		public readonly Dictionary<string, object> payload;

		public string GetString(string key) => Convert.ToString(payload[key]);
		public int GetInt(string key) => Convert.ToInt32(payload[key]);
		public float GetFloat(string key) => Convert.ToSingle(payload[key]);
		public bool GetBool(string key) => Convert.ToBoolean(payload[key]);

		public Vector3 GetVector3(string xKey, string yKey, string zKey) {
			return new Vector3(
				GetFloat(xKey),
				GetFloat(yKey),
				GetFloat(zKey));
		}
 
		public Message(string topic, Dictionary<string, object> payload) {
			this.topic = topic;
			this.payload = payload;
		}
	}
}