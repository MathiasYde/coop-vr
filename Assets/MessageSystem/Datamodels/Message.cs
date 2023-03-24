using System;
using System.Collections.Generic;
using UnityEngine;

namespace MessageSystem {
	public class Message {
		public readonly string topic;
		public readonly Dictionary<string, object> payload;

		public string GetString(string key) { return Convert.ToString(payload[key]); }
		public int GetInt(string key) { return Convert.ToInt32(payload[key]); }
		public float GetFloat(string key) { return Convert.ToSingle(payload[key]); }

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