using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEditor;
using UnityEngine;

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
	private static List<string> scenes;

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

		if (windowsBuild)
			BuildWindows ();

		if (macOSXBuild)
			BuildMacOSX ();

		if(linuxBuild)
			BuildLinux ();
	}


	private static void ReadBuildProperties () {
		Debug.Log ("Reading Build Properties");
		XmlDocument xmlDoc = new XmlDocument();

		if (!File.Exists(buildPropertiesPath))
			return;
		
		xmlDoc.Load(buildPropertiesPath); // load the file.
		XmlNode xmlBuildProperties = xmlDoc.SelectSingleNode("Properties");

		//BuildProperties
		XmlNode xmlBuildProject		= xmlBuildProperties.SelectSingleNode("Project"); 
		buildName					= xmlBuildProject.SelectSingleNode("Name").InnerText;
		buildVersion 				= xmlBuildProject.SelectSingleNode("Version").InnerText;
		windowsBuild				= (xmlBuildProject.SelectSingleNode("WindowsVersion").InnerText == "true");
		macOSXBuild					= (xmlBuildProject.SelectSingleNode("MacOSXVersion").InnerText == "true");
		linuxBuild					= (xmlBuildProject.SelectSingleNode("LinuxVersion").InnerText == "true");

		//BuildScenes
		XmlNode xmlBuildScenes	= xmlBuildProperties.SelectSingleNode("Scenes");
		scenes = new List<string>();

		foreach (XmlNode buildScene in xmlBuildScenes) {
			scenes.Add(buildScene.InnerText);
		}

		//BuildPaths
		XmlNode xmlBuildPaths		= xmlBuildProperties.SelectSingleNode("Paths");
		originProjectPath			= xmlBuildPaths.SelectSingleNode("OriginProjectPath").InnerText;
		destinationProjectPath		= xmlBuildPaths.SelectSingleNode("DestinationProjectPath").InnerText;
		compresorPath				= xmlBuildPaths.SelectSingleNode("CompresorPath").InnerText;

		//BuildExtraFiles
		XmlNode xmlExtraFiles 		= xmlBuildProperties.SelectSingleNode("ExtraFiles");
		foreach (XmlNode buildFile in xmlExtraFiles) {
		
		}
	}

	private static void BuildWindows () {
		Debug.Log ("Starting Windows Build");
		auxPath = originProjectPath + "/MonkeyTV_" + buildVersion + "_Windows/" + buildName + ".exe";
		BuildPipeline.BuildPlayer( scenes.ToArray(), auxPath, BuildTarget.StandaloneWindows64, BuildOptions.None);
		Debug.Log ("Windows Build Ended");
	}

	private static void BuildMacOSX () {
		Debug.Log ("Starting MacOSX Build");
		auxPath = originProjectPath + "/MonkeyTV_" + buildVersion + "_MacOSX/" + buildName + ".app";
		Debug.Log ("MacOSX Build Ended");
	}

	private static void BuildLinux () {
		Debug.Log ("Starting Linux Build");
		auxPath = originProjectPath + "/MonkeyTV_" + buildVersion + "_Linux/" + buildName + ".x86";
		BuildPipeline.BuildPlayer( scenes.ToArray(), auxPath, BuildTarget.StandaloneLinuxUniversal, BuildOptions.None);
		Debug.Log ("Linux Build Ended");
	}

	private static void CopyFile() {
		
	}
}