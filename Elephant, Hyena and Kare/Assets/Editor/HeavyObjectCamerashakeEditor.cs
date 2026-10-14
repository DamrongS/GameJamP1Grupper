
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HeavyObjectCamerashake))]
public class HeavyObjectCamerashakeEditor : Editor
{
    private SerializedProperty shape;
    private SerializedProperty duration;
    private SerializedProperty intensity;
    private SerializedProperty customCurve;

    private void OnEnable()
    {
        shape = serializedObject.FindProperty("impulseShape");
        duration = serializedObject.FindProperty("impulseDuration");
        intensity = serializedObject.FindProperty("shakeIntensity");
        customCurve = serializedObject.FindProperty("customCurve");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawPropertiesExcluding(
            serializedObject,
            "m_Script",
            "customCurve"
        );

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField(
            "Impulse Preview",
            EditorStyles.boldLabel
        );

        string shapeName = shape != null
            ? shape.enumNames[shape.enumValueIndex]
            : "Explosion";

        if (shapeName == "Custom")
        {
            DrawCustomCurve();
        }
        else
        {
            DrawWaveform(shapeName);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawCustomCurve()
    {
        if (customCurve == null)
        {
            EditorGUILayout.HelpBox(
                "Add a serialized AnimationCurve named customCurve " +
                "to HeavyObjectCamerashake.cs.",
                MessageType.Warning
            );
            return;
        }

        // Create an editable curve preview
        Rect rect = GUILayoutUtility.GetRect(
            10, 130, GUILayout.ExpandWidth(true)
        );

        // Dark background
        EditorGUI.DrawRect(
            rect,
            new Color(0.15f, 0.15f, 0.15f)
        );

        // Draw an editable AnimationCurve
        EditorGUI.CurveField(
            rect,
            customCurve,
            new Color(1f, 0.75f, 0f),
            new Rect(0f, -1f, 1f, 2f)
        );

        EditorGUILayout.HelpBox(
            "Click the graph to edit the curve. " +
            "You can add keyframes and adjust tangent handles.",
            MessageType.Info
        );
    }

    private void DrawWaveform(string shapeName)
    {
        Rect rect = GUILayoutUtility.GetRect(
            10, 130, GUILayout.ExpandWidth(true)
        );

        // Draw background
        EditorGUI.DrawRect(
            rect,
            new Color(0.15f, 0.15f, 0.15f)
        );

        float centerY = rect.center.y;

        // Draw horizontal center line
        EditorGUI.DrawRect(
            new Rect(rect.x, centerY, rect.width, 1),
            new Color(0.35f, 0.35f, 0.35f)
        );

        int samples = 150;
        Vector3[] points = new Vector3[samples];

        float seconds = duration != null
            ? Mathf.Max(0.01f, duration.floatValue)
            : 0.4f;

        float strength = intensity != null
            ? intensity.vector3Value.magnitude
            : 0.1f;

        float amplitude = Mathf.Clamp(
            strength / 0.1f, 0.1f, 1.5f
        );

        for (int i = 0; i < samples; i++)
        {
            float t = i / (float)(samples - 1);

            float value = EvaluateShape(shapeName, t);

            float x = rect.x + t * rect.width;

            float y = centerY -
                value * (rect.height * 0.45f) * amplitude;

            points[i] = new Vector3(x, y, 0);
        }

        // Draw yellow waveform
        Handles.BeginGUI();
        Handles.color = new Color(1f, 0.75f, 0f);
        Handles.DrawAAPolyLine(2f, points);
        Handles.EndGUI();

        EditorGUILayout.LabelField(
            $"Approximate waveform | {seconds:0.00}s",
            EditorStyles.miniLabel
        );
    }

    private float EvaluateShape(string shapeName, float t)
    {
        switch (shapeName)
        {
            case "Recoil":
                return 1f - Mathf.SmoothStep(0f, 1f, t);

            case "Bump":
                if (t < 0.23f)
                {
                    float x = t / 0.23f;
                    return -0.45f * Mathf.Sin(x * Mathf.PI);
                }
                else
                {
                    float x = (t - 0.23f) / 0.77f;
                    return Mathf.Sin(x * Mathf.PI);
                }

            case "Explosion":
                return
                    -0.28f * Gaussian(t, 0.07f, 0.065f)
                    + 1.00f * Gaussian(t, 0.36f, 0.09f)
                    - 0.46f * Gaussian(t, 0.55f, 0.055f)
                    + 0.27f * Gaussian(t, 0.69f, 0.055f)
                    - 0.12f * Gaussian(t, 0.80f, 0.035f)
                    + 0.09f * Gaussian(t, 0.89f, 0.035f);

            case "Rumble":
                float wave = Mathf.Sin(t * 5f * Mathf.PI);

                float envelope = 0.2f +
                    0.8f * Mathf.Pow(
                        Mathf.Sin(t * Mathf.PI), 2f
                    );

                return wave * wave * envelope;

            default:
                return 0f;
        }
    }

    private float Gaussian(float t, float center, float width)
    {
        float x = (t - center) / width;
        return Mathf.Exp(-0.5f * x * x);
    }
}
