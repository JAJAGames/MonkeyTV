using UnityEngine;
using System.Collections;
using System;
using UnityEditor;
using System.IO;
using System.Xml;

public class BuildCreator {

	private static string buildPropertiesPath = Application.dataPath + "/Editor/BuildManager/BuildProperties.xml";
	private static string auxPath;

		//BuildProperties
	private static string buildName;
	private static string buildVersion;
	private static Boolean windowsBuild = false;
	private static Boolean macOSXBuild = false;
	private static Boolean linuxBuild = false;

	//BuildScenes
	private static string[] scenes;

	//BuildPaths
	private static string originProjectPath;
	private static string destinationProjectPath;
	private static string compresorPath;

	//BuildExtraFiles

	public static void SceneSelector() {
		//Debug.Log (PlayerSettings.bundleVersion);
	}

	public static void BuildConfiguration() {

	}

	public static void BuildGame() {
		ReadBuildProperties ();
		/*
		if (windowsBuild)
			BuildWindows ();

		if (macOSXBuild)
			BuildMacOSX ();

		if(linuxBuild)
			BuildLinux ();
			*/
	}


	private static void ReadBuildProperties () {
		Debug.Log ("Reading Build Properties");
		XmlDocument xmlDoc = new XmlDocument();

		if (!File.Exists(buildPropertiesPath))
			return;
		
		xmlDoc.Load(buildPropertiesPath); // load the file.
		XmlNode xmlBuildProperties	= xmlDoc.GetElementById("Properties"); 

		//BuildProperties
		XmlNode xmlBuildProject		= xmlBuildProperties.SelectSingleNode("Project"); 
		buildName					= xmlBuildProject.SelectSingleNode("Name").InnerText;
		buildVersion 				= xmlBuildProject.SelectSingleNode("Version").InnerText;
		windowsBuild				= (xmlBuildProject.SelectSingleNode("WindowsVersion").InnerText == "true");
		macOSXBuild					= (xmlBuildProject.SelectSingleNode("MacOSXVersion").InnerText == "true");
		linuxBuild					= (xmlBuildProject.SelectSingleNode("LinuxVersion").InnerText == "true");

		//BuildScenes
		XmlNodeList xmlBuildScenes	= xmlDoc.GetElementsByTagName("Scenes");
		scenes = new string[xmlBuildScenes.Count];
		int i = 0;
		foreach (XmlNode buildScene in xmlBuildScenes) {
			scenes[i] = buildScene.InnerText;
			++i;
		}

		//BuildPaths
		XmlNode xmlBuildPaths		= xmlDoc.GetElementById("Paths");
		originProjectPath			= xmlBuildProperties.SelectSingleNode("OriginProjectPath").InnerText;
		destinationProjectPath		= xmlBuildProperties.SelectSingleNode("DestinationProjectPath").InnerText;
		compresorPath				= xmlBuildProperties.SelectSingleNode("CompresorPath").InnerText;

		//BuildExtraFiles
		XmlNodeList xmlExtraFiles 	= xmlDoc.GetElementsByTagName ("ExtraFiles");
		foreach (XmlNode buildFile in xmlExtraFiles) {
		
		}
	}

	private static void BuildWindows () {
		Debug.Log ("Starting Windows Build");
		auxPath = originProjectPath + "/MonkeyTV_" + buildVersion + "_Windows/" + buildName + ".exe";
		BuildPipeline.BuildPlayer( scenes, auxPath, BuildTarget.StandaloneWindows64, BuildOptions.None);
	}

	private static void BuildMacOSX () {
		Debug.Log ("Starting MacOSX Build");
		auxPath = originProjectPath + "/MonkeyTV_" + buildVersion + "_MacOSX/" + buildName + ".app";
		BuildPipeline.BuildPlayer( scenes, auxPath, BuildTarget.StandaloneOSXIntel, BuildOptions.None);
	}

	private static void BuildLinux () {
		Debug.Log ("Starting Linux Build");
		auxPath = originProjectPath + "/MonkeyTV_" + buildVersion + "_Linux/" + buildName + ".x86";
		BuildPipeline.BuildPlayer( scenes, auxPath, BuildTarget.StandaloneLinuxUniversal, BuildOptions.None);
	}

	private static void CopyFile() {
		
	}
}