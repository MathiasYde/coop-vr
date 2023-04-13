using UnityEngine;
using UnityEngine.Events;

public class TriggerCollider : MonoBehaviour {
	[SerializeField] private Optional<string> targetTag;
	
	[Header("Events")]
	[SerializeField] private UnityEvent onTriggerEnterEvent;
	[SerializeField] private UnityEvent onTriggerExitEvent;

	private void Awake() {
		onTriggerEnterEvent ??= new UnityEvent();
		onTriggerExitEvent ??= new UnityEvent();
	}

	private void OnTriggerEnter(Collider other) {
		if (targetTag.Enabled && other.CompareTag(targetTag.Value))
			onTriggerEnterEvent?.Invoke();
	}

	private void OnTriggerExit(Collider other) {
		if (targetTag.Enabled && other.CompareTag(targetTag.Value))
			onTriggerExitEvent?.Invoke();
	}
}
