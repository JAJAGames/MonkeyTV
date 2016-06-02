using UnityEngine;
using UnityEditor;

public class ToolsMenuItem : MonoBehaviour {
	[MenuItem("Eines/Selector d'escenes")]
	public static void SceneSelector(MenuCommand command) {
		BuildCreator.SceneSelector();
	}

	[MenuItem("Eines/Configuració")]
	private static void BuildConfiguration(MenuCommand command) {
		BuildCreator.BuildConfiguration();
	}

	[MenuItem("Eines/Crear Builds")]
	public static void BuildGame(MenuCommand command) {
		BuildCreator.BuildGame();
	}		
}
