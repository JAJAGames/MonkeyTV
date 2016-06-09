using UnityEngine;
using UnityEditor;

public class ToolsMenuItem : MonoBehaviour {
	[MenuItem("Eines/Crear Builds")]
	public static void BuildGame(MenuCommand command) {
		BuildCreator.BuildGame();
	}		
}
