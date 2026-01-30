using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LjudChef))]
public class LjudChefInspektor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        LjudChef ljudChef = (LjudChef)target;

        if (ljudChef.GetEventInstances() is { } eventInstances)
        {
            if (eventInstances.Count > 0)
            {
                GUILayout.Label("Active FMOD Events:", EditorStyles.boldLabel);
            }
            else
            {
                GUILayout.Label("No active FMOD events found. (Not in play mode?)", EditorStyles.boldLabel);
            }
            foreach (var kvp in eventInstances)
            {
                kvp.ljudinstans.getDescription(out EventDescription description);
                description.getPath(out string path);
                kvp.ljudinstans.getPlaybackState(out PLAYBACK_STATE state);
                
                EditorGUILayout.BeginVertical("box");
                
                kvp.ljudinstans.getPaused(out bool isPaused);
                
                EditorGUILayout.LabelField("Event Path:", path, EditorStyles.boldLabel);
                EditorGUILayout.LabelField("GUID:", kvp.ljudref.ToString(), EditorStyles.miniLabel);
                
                string stateText = isPaused ? "PAUSED" : state.ToString();

                EditorGUILayout.BeginHorizontal();
                float labelWidth = EditorGUIUtility.labelWidth;
                EditorGUILayout.LabelField("Playback State:", EditorStyles.label, GUILayout.Width(labelWidth));
                
                Color stateTextColor = Color.white;
                
                switch (state)
                {
                    case PLAYBACK_STATE.PLAYING: stateTextColor = Color.green; break;
                    case PLAYBACK_STATE.STOPPED: stateTextColor = Color.red; break;
                    case PLAYBACK_STATE.SUSTAINING: stateTextColor = new Color(1.0f, 0.7f, 0.85f); break;
                    case PLAYBACK_STATE.STARTING: stateTextColor = Color.cyan; break;
                    case PLAYBACK_STATE.STOPPING: stateTextColor = new Color(1f, 0.5f, 0f); break;
                }
                if (isPaused) stateTextColor = Color.yellow;
                
                GUI.color = stateTextColor;
                EditorGUILayout.LabelField(stateText, EditorStyles.miniBoldLabel);
                GUI.color = Color.white;
                EditorGUILayout.EndHorizontal();
                
                DisplayEventParameters(kvp.ljudinstans, description);

                if (GUILayout.Button("Stop Event"))
                {
                    //LjudChef.Instans.StopEvent(kvp.Value);
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space(0.5f);
            }
        }
        else
        {
            GUILayout.Label("No active FMOD events found. (Not in play mode?)", EditorStyles.boldLabel);
        }
        if (Application.isPlaying)
        {
            Repaint();
        }
    }
    
    private string GetEventPath(EventDescription desc)
    {
        desc.getPath(out string path);
        return string.IsNullOrEmpty(path) ? "[Unknown Path]" : path;
    }

    private void DisplayEventParameters(EventInstance eventInstance, EventDescription eventDescription)
    {
        eventDescription.getParameterDescriptionCount(out int count);
        if (count > 0)
        {
            EditorGUILayout.LabelField("Parameters:", EditorStyles.boldLabel);
        }

        for (int i = 0; i < count; i++)
        {
            eventDescription.getParameterDescriptionByIndex(i, out PARAMETER_DESCRIPTION parameterDescription);
            eventInstance.getParameterByID(parameterDescription.id, out float paramValue);
            bool labeled = parameterDescription.flags.HasFlag(PARAMETER_FLAGS.LABELED);
            
            if (labeled)
            {
                eventDescription.getParameterLabelByIndex(i, (int)paramValue, out string label);
                
                EditorGUILayout.LabelField("    " + parameterDescription.name, label, EditorStyles.boldLabel);
                continue;
            }
            
            EditorGUILayout.LabelField("    " + parameterDescription.name + ":", $"{paramValue}", EditorStyles.miniBoldLabel);
            

        }
    }
}
