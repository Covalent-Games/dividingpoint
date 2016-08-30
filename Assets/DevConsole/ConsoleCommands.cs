using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DevConsole;

public class ConsoleCommands : MonoBehaviour {

	void Start(){
		Console.AddCommand(new Command<string>("TIME_TIMESCALE", TimeScale));
		Console.AddCommand(new Command<string>("TIME_SHOWTIME", ShowTime));
		Console.AddCommand(new Command<string>("HELP",ExampleCommand, ExampleCommandHelp));
	}
    static void ExampleCommand(string args){
		Console.Log("Type HELP? to use this command");
	}
	static void ExampleCommandHelp(){
		string unColoredText = "The help for this command is shown through a custom method";
		while (unColoredText != string.Empty){
			string coloredText = string.Empty;
			int i = 0;
			for (i = 0; i < unColoredText.Length; i++){
				if (unColoredText[i] == ' ')
					break;
				coloredText+=unColoredText[i];
			}
			Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
			Console.Log(coloredText, color);
			unColoredText = unColoredText.Substring(Mathf.Min(unColoredText.Length,i+1));
		};
	}
	static void TimeScale(string sValue){
		float fValue;
		if (float.TryParse(sValue, out fValue)){
			Time.timeScale = fValue;
			Console.Log("Change successful", Color.green);
		}
		else
			Console.LogError("The entered value is not a valid float value");
	}
	static void ShowTime(string args){
		Console.Log(Time.time.ToString());
	}
}