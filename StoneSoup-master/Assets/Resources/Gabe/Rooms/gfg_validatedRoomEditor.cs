using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(gfg_validatedRoom))]
public class Class6ValidatedRoomEditor : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        gfg_validatedRoom myRoom = (gfg_validatedRoom)target;
        if (GUILayout.Button("Validate Room")) {
            myRoom.validateRoom();
        }
    }
}

