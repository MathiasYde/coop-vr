using System.Collections.Generic;

namespace MessageSystem {
	public class Message {
		public readonly string topic;
		public readonly Dictionary<string, object> payload;

		public Message(string topic, Dictionary<string, object> payload) {
			this.topic = topic;
			this.payload = payload;
		}
	}
}