using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class App : MonoBehaviour {
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void Bootstrap() {
		GameObject app = Object.Instantiate(Resources.Load("App")) as GameObject;

		if (app == null)
			throw new ApplicationException();
		
		Object.DontDestroyOnLoad(app);
	}
}
