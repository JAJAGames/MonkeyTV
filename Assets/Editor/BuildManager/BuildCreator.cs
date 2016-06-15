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
	private static string completeName;
	private static Boolean windowsBuild = false;
	private static Boolean macOSXBuild = false;
	private static Boolean linuxBuild = false;

	//BuildScenes
	private static List<string> scenes;

	//BuildPaths
	private static string originProjectPath;
	private static string destinationProjectPath;
	private static string compressorPath;

	//BuildExtraFiles
	private static List<string> fileName;
	private static List<string> extraFilesLocation;
	private static List<string> extraFilesDestinationWindows;
	private static List<string> extraFilesDestinationMacOSX;
	private static List<string> extraFilesDestinationLinux;
	private static int filesLength;


	public static void BuildGame() {
		ReadBuildProperties ();

		if (windowsBuild) {
			CreateBuild ("Windows", BuildTarget.StandaloneWindows64);
			CopyFiles ("Windows", extraFilesDestinationWindows);
			CompressBuild("Windows", buildName + ".exe " + buildName + "_Data");
		}

		if (macOSXBuild) {
			CreateBuild ("MacOSX", BuildTarget.StandaloneOSXUniversal);
			CopyFiles ("MacOSX", extraFilesDestinationMacOSX);
			CompressBuild("MacOSX", buildName + ".app");
		}

		if(linuxBuild) {
			CreateBuild ("Linux", BuildTarget.StandaloneLinuxUniversal);
			CopyFiles ("Linux", extraFilesDestinationLinux);
			CompressBuild("Linux", buildName + ".x86 " + buildName + "_Data");
		}
	}

	public static void UploadBuilds() {
		ReadUploadProperties ();

		if (windowsBuild)
			UploadFile ("Windows");

		if (macOSXBuild)
			UploadFile ("MacOSX");

		if(linuxBuild)
			UploadFile ("Linux");
	}


	private static void ReadBuildProperties () {
		Debug.Log ("Reading Build Properties");
		XmlDocument xmlDoc = new XmlDocument();

		if (!File.Exists(buildPropertiesPath))
			return;
		
		xmlDoc.Load(buildPropertiesPath); // load the file.
		XmlNode xmlBuildProperties 	= xmlDoc.SelectSingleNode("Properties");

		//BuildProperties
		XmlNode xmlBuildProject		= xmlBuildProperties.SelectSingleNode("Project"); 
		buildName					= xmlBuildProject.SelectSingleNode("Name").InnerText;
		buildVersion 				= xmlBuildProject.SelectSingleNode("Version").InnerText;
		completeName 				= buildName + "_" + buildVersion;
		windowsBuild				= (xmlBuildProject.SelectSingleNode("WindowsVersion").InnerText == "true");
		macOSXBuild					= (xmlBuildProject.SelectSingleNode("MacOSXVersion").InnerText == "true");
		linuxBuild					= (xmlBuildProject.SelectSingleNode("LinuxVersion").InnerText == "true");

		//BuildScenes
		XmlNode xmlBuildScenes		= xmlBuildProperties.SelectSingleNode("Scenes");
		scenes 						= new List<string>();

		foreach (XmlNode buildScene in xmlBuildScenes) {
			scenes.Add(buildScene.InnerText);
		}

		//BuildPaths
		XmlNode xmlBuildPaths		= xmlBuildProperties.SelectSingleNode("Paths");
		originProjectPath			= xmlBuildPaths.SelectSingleNode("OriginProjectPath").InnerText;
		destinationProjectPath		= AbsolutePath (xmlBuildPaths.SelectSingleNode("DestinationProjectPath").InnerText + completeName);
		compressorPath				= xmlBuildPaths.SelectSingleNode("CompressorPath").InnerText;

		//BuildExtraFiles
		XmlNode xmlExtraFiles 		= xmlBuildProperties.SelectSingleNode("ExtraFiles");
		fileName 					= new List<string> ();
		extraFilesLocation 			= new List<string>();
		extraFilesDestinationWindows= new List<string>();
		extraFilesDestinationMacOSX	= new List<string>();
		extraFilesDestinationLinux	= new List<string>();

		filesLength = 0;
		foreach (XmlNode buildExtraFile in xmlExtraFiles) {
			fileName.Add(buildExtraFile.SelectSingleNode("FileName").InnerText);
			extraFilesLocation.Add(buildExtraFile.SelectSingleNode("LocationFilePath").InnerText);
			extraFilesDestinationWindows.Add(buildExtraFile.SelectSingleNode("DestinationWindowsFilePath").InnerText);
			extraFilesDestinationMacOSX.Add(buildExtraFile.SelectSingleNode("DestinationMacOSXFilePath").InnerText);
			extraFilesDestinationLinux.Add(buildExtraFile.SelectSingleNode("DestinationLinuxFilePath").InnerText);
			++filesLength;
		}
	}

	private static void ReadUploadProperties () {
		Debug.Log ("Reading Upload Properties");
		XmlDocument xmlDoc = new XmlDocument();

		if (!File.Exists(buildPropertiesPath))
			return;

		xmlDoc.Load(buildPropertiesPath); // load the file.
		XmlNode xmlBuildProperties 	= xmlDoc.SelectSingleNode("Properties");

		//BuildProperties
		XmlNode xmlBuildProject		= xmlBuildProperties.SelectSingleNode("Project"); 
		buildName					= xmlBuildProject.SelectSingleNode("Name").InnerText;
		buildVersion 				= xmlBuildProject.SelectSingleNode("Version").InnerText;
		completeName 				= buildName + "_" + buildVersion;
		windowsBuild				= (xmlBuildProject.SelectSingleNode("WindowsVersion").InnerText == "true");
		macOSXBuild					= (xmlBuildProject.SelectSingleNode("MacOSXVersion").InnerText == "true");
		linuxBuild					= (xmlBuildProject.SelectSingleNode("LinuxVersion").InnerText == "true");

		//BuildPaths
		XmlNode xmlBuildPaths		= xmlBuildProperties.SelectSingleNode("Paths");
		originProjectPath			= xmlBuildPaths.SelectSingleNode("OriginProjectPath").InnerText;
		destinationProjectPath		= AbsolutePath (xmlBuildPaths.SelectSingleNode("DestinationProjectPath").InnerText + completeName);
	}

	private static void CreateBuild (string platformName, BuildTarget platformTarget) {
		Debug.Log ("Starting " + platformName + " Build");
		auxPath = originProjectPath + "/" + completeName + "_" + platformName + "/" + buildName + ".exe";
		BuildPipeline.BuildPlayer(scenes.ToArray(), auxPath, platformTarget, BuildOptions.None);
		Debug.Log (platformName + " Build Ended");
	}

	private static void CopyFiles(string platform, List<string> filesDestination) {
		originProjectPath = AbsolutePath (originProjectPath);

		for (int i = 0; i < filesLength; ++i) {
		
			if (!System.IO.Directory.Exists (originProjectPath + completeName + "_" + platform + filesDestination[i]))
				System.IO.Directory.CreateDirectory (originProjectPath + completeName + "_" + platform + filesDestination[i]);

			try {
				FileUtil.CopyFileOrDirectory(Application.dataPath + extraFilesLocation[i] + fileName[i], originProjectPath + completeName + "_" + platform + filesDestination[i] + fileName[i]);
			} catch (Exception e) {
				Debug.LogError(fileName[i] + " Copy failed: " + e.ToString());
			}
		}
	}

	private static void CompressBuild(string platform, string objectives) {
		System.Diagnostics.Process proc = new System.Diagnostics.Process();

		try {
			proc.StartInfo.FileName = compressorPath;

			proc.StartInfo.WorkingDirectory = originProjectPath + completeName + "_" + platform;

			proc.StartInfo.Arguments = "-9 -r " + completeName + "_" + platform + ".zip " + objectives;

			proc.StartInfo.UseShellExecute = false;
			proc.StartInfo.CreateNoWindow = true;

			proc.Start();

			Debug.Log(platform + " Compression done");
		} catch (Exception e) {
			Debug.Log(platform + " Compression FAILED: " + e.ToString());
		}
	}

	private static void UploadFile(string platform) {
		if (!System.IO.Directory.Exists (destinationProjectPath))
			System.IO.Directory.CreateDirectory (destinationProjectPath);

		try {
			FileUtil.CopyFileOrDirectory(AbsolutePath(originProjectPath + completeName + "_" + platform + "/" + completeName + "_" + platform + ".zip"), destinationProjectPath + "\\" + completeName + "_" + platform + ".zip");

			Debug.Log(completeName + "_" + platform + ".zip Uploaded in: " + destinationProjectPath);
		} catch (Exception e) {
			Debug.LogError(completeName + "_" + platform + ".zip Upload failed: " + e.ToString());
		}
	}

	private static string AbsolutePath(string path) {
		return Path.GetFullPath(path);
	}
}