using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateScriptableObject {

    [MenuItem("Assets/Create/Scriptable Object/RatioCurve", false, 1000)]


	static void Create () 
    {
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<RatioCurve>(), "Assets/NewScriptableObject.asset");
	}
}
