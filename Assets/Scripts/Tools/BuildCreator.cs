using UnityEngine;
using System.Collections;
using System;
using UnityEditor;

public class BuildCreator {

	string path = "/Users/Done/Documents/Fuga_Builds";
	string gdrive = "/Users/Done/gdrive/Fuga_Builds";
	//string name = String.Format("EFTI_{1:s}_{0:d_MMMM_HH.mm}_{2:s}", DateTime.Now, PlayerSettings.bundleVersion, (development));

	//Get filename
	/*
		

		
		string[] levels = new string[] { "Assets/TENOPIA/Scenes/mapa.unity" };

		Debug.Log ("Building " + path + name + "_Win.exe");

		CopyFiles ();

		//Build player
		Debug.Log("Building Win x64 build");
		//BuildPipeline.BuildPlayer(levels, path + name + "_Win.exe", BuildTarget.StandaloneWindows64, (development ? BuildOptions.DeviceType);
	*/

	public static void SceneSelector() {
		Debug.Log (PlayerSettings.bundleVersion);
	}

	public static void BuildConfiguration() {

	}

	public static void BuildGame() {

	}
}
