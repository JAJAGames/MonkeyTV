using UnityEngine;
using UnityEditor;

public class ToolsMenuItem : MonoBehaviour {
	[MenuItem("Eines/Crear Builds")]
	public static void BuildGame(MenuCommand command) {
		BuildCreator.BuildGame();
	}

	[MenuItem("Eines/Pujar Builds")]
	public static void UploadBuilds(MenuCommand command) {
		BuildCreator.UploadBuilds();
	}	
}
