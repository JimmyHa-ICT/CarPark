using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistic
{
    public static Dictionary<string, int> Fields;
    public static Dictionary<string, int> FieldsInScene;
    public static Dictionary<string, int> FieldsGlobal;

    public enum Mode
    {
        InStage = 0,
        InScene = 1,
        Global = 2
    }

    public static void Init()
    {
        Fields = new Dictionary<string, int>();
        FieldsInScene = new Dictionary<string, int>();
        FieldsGlobal = new Dictionary<string, int>();
    }

    public static void ResetAll()
    {
        Debug.Log("Clear statistic");
        Fields.Clear();
    }

    public static void ResetInScene()
    {
        FieldsInScene.Clear();
    }

    public static int GetField(string key)
    {
        if (Fields.ContainsKey(key))
            return Fields[key];
        else if (FieldsInScene.ContainsKey(key))
            return FieldsInScene[key];
        else if (FieldsGlobal.ContainsKey(key))
            return FieldsGlobal[key];
        else
            return 0;
    }

    public static void SetField(string key, int value, Mode mode = Mode.InStage)
    {
        if (mode == Mode.InStage)
        {
            if (!Fields.ContainsKey(key))
                Fields.Add(key, value);
            else
                Fields[key] = value;
        }
        else if (mode == Mode.InScene)
        {
            if (!FieldsInScene.ContainsKey(key))
                FieldsInScene.Add(key, value);
            else
                FieldsInScene[key] = value;
        }
        else if (mode == Mode.Global)
        {
            if (!FieldsGlobal.ContainsKey(key))
                FieldsGlobal.Add(key, value);
            else
                FieldsGlobal[key] = value;
        }    
    }
}
