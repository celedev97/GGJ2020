using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Game : MonoBehaviour {
    public static Dictionary<string, object> variables = new Dictionary<string, object>();

    public static void PlaySound(string path) {
        Debug.Log("SOUND: " + path);
    }

    public void playSound(string path) {
        PlaySound(path);
    }

    public static void SaveVariable(string variable_name, object value) {
        variables[variable_name] = value;
    }

    public void saveVariable(string variable_name, object value) {
        SaveVariable(variable_name, value);
    }

    public static object GetVariable(string variable_name) {
        if (variables.ContainsKey(variable_name)) {
            return variables[variable_name];
        } else {
            return null;
        }
    }

    public object getVariable(string variable_name) {
        return GetVariable(variable_name);
    }

    private static string lastLog = "";
    internal static void Log(string log) {
        if (log != lastLog) {
            Debug.Log(log);
            lastLog = log;
        }
    }
}
