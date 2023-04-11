using UnityEngine;
using UnityEngine.Events;

public class TriggerCollider : MonoBehaviour {
	[SerializeField] private string tag;
	
	[Header("Events")]
	[SerializeField] private UnityEvent onTriggerEnterEvent;
	[SerializeField] private UnityEvent onTriggerExitEvent;

	private void Awake() {
		onTriggerEnterEvent ??= new UnityEvent();
		onTriggerExitEvent ??= new UnityEvent();
	}

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag(tag))
			onTriggerEnterEvent?.Invoke();
	}

	private void OnTriggerExit(Collider other) {
		if (other.CompareTag(tag))
			onTriggerExitEvent?.Invoke();
	}
}
