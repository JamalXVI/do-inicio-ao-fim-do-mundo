using UnityEngine;
using System.Collections.Generic;

public class Game_Input : Game_Base {
    public static Game_Input game_input;
    void Awake() {
        if (Game_Input.game_input == null)
        {
            DontDestroyOnLoad(gameObject);
            game_input = this;
        }
        else if (Game_Player.instancia != this)
        {

            Destroy(gameObject);
        }
    }
    /*
        // Use this for initialization
        void Start () {

        }

        // Update is called once per frame
        void Update () {

        }
        private static void LimparTeclas()
        {
            SerializedObject serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
            SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");
            axesProperty.ClearArray();
            serializedObject.ApplyModifiedProperties();
        }
        private static SerializedProperty GetChildProperty(SerializedProperty parent, string name)
        {
            SerializedProperty child = parent.Copy();
            child.Next(true);
            do
            {
                if (child.name == name) return child;
            }
            while (child.Next(false));
            return null;
        }
        private static bool AxisDefined(string axisName)
        {
            SerializedObject serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
            SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");

            axesProperty.Next(true);
            axesProperty.Next(true);
            while (axesProperty.Next(false))
            {
                SerializedProperty axis = axesProperty.Copy();
                axis.Next(true);
                if (axis.stringValue == axisName) return true;
            }
            return false;
        }
        public static Dictionary<TKey, KeyCode> ClonarLista<TKey, KeyCode>
       (Dictionary<TKey, KeyCode> original)
        {
            Dictionary<TKey, KeyCode> ret = new Dictionary<TKey, KeyCode>(original.Count,
                                                                    original.Comparer);
            foreach (KeyValuePair<TKey, KeyCode> entry in original)
            {
                ret.Add(entry.Key, (KeyCode)entry.Value);

            }
            return ret;
        }
        public static void TranscreverControles()
        {
            LimparTeclas();
            Dictionary<string, KeyCode> t = ClonarLista(Game_Player.game_player.Teclas);
            Dictionary<string, string> v = new Dictionary<string, string>();
            foreach (var tecla in t)
            {
                v.Add(tecla.Key, tecla.Value.ToString().ToLower().Replace("arrow", ""));
            }
            AddAxis(new InputAxis()
            {
                name = "Horizontal",
                sensitivity = 3f,
                negativeButton = v["esquerda"],
                positiveButton = v["direita"],
                type = AxisType.KeyOrMouseButton,
                axis = 1,
                dead = 0.001f,
                snap = true,
                gravity = 3f,
                invert = false
            });
            AddAxis(new InputAxis()
            {
                name = "Vertical",
                sensitivity = 3f,
                gravity = 3f,
                negativeButton = "s",
                positiveButton = "w",
                type = AxisType.KeyOrMouseButton,
                axis = 1,
                dead = 0.001f,
                snap = true,
                invert = false
            });
            AddAxis(new InputAxis()
            {
                name = "Fire1",
                sensitivity = 1000f,
                positiveButton = v["habilidade"],
                altPositiveButton = "mouse 0",
                type = AxisType.KeyOrMouseButton,
                axis = 1,
                dead = 0.001f,
                gravity = 1000f,
                snap = false,
                invert = false
            });
            AddAxis(new InputAxis()
            {
                name = "Fire2",
                sensitivity = 1000f,
                positiveButton = v["troca"],
                altPositiveButton = "mouse 0",
                type = AxisType.KeyOrMouseButton,
                axis = 1,
                dead = 0.001f,
                gravity = 1000f,
                snap = false,
                invert = false
            });
            AddAxis(new InputAxis()
            {
                name = "Jump",
                sensitivity = 1000f,
                positiveButton = v["pulo"],
                type = AxisType.KeyOrMouseButton,
                axis = 1,
                dead = 0.001f,
                gravity = 1000f,
                snap = false,
                invert = false
            });
            AddAxis(new InputAxis()
            {
                name = "Correr",
                sensitivity = 1000f,
                positiveButton = v["correr"],
                type = AxisType.KeyOrMouseButton,
                axis = 1,
                dead = 0.001f,
                gravity = 1000f,
                snap = false,
                invert = false
            });
            AddAxis(new InputAxis()
            {
                name = "Submit",
                sensitivity = 1000f,
                positiveButton = v["enter"],
                type = AxisType.KeyOrMouseButton,
                axis = 1,
                dead = 0.001f,
                gravity = 1000f,
                snap = false,
                invert = false
            });
            AddAxis(new InputAxis()
            {
                name = "Cancel",
                sensitivity = 1000f,
                positiveButton = "escape",
                type = AxisType.KeyOrMouseButton,
                axis = 1,
                dead = 0.001f,
                gravity = 1000f,
                snap = false,
                invert = false
            });

        }
        private static void AddAxis(InputAxis axis)
        {
            if (AxisDefined(axis.name)) return;

            SerializedObject serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
            SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");

            axesProperty.arraySize++;
            serializedObject.ApplyModifiedProperties();

            SerializedProperty axisProperty = axesProperty.GetArrayElementAtIndex(axesProperty.arraySize - 1);

            GetChildProperty(axisProperty, "m_Name").stringValue = axis.name;
            GetChildProperty(axisProperty, "descriptiveName").stringValue = axis.descriptiveName;
            GetChildProperty(axisProperty, "descriptiveNegativeName").stringValue = axis.descriptiveNegativeName;
            GetChildProperty(axisProperty, "negativeButton").stringValue = axis.negativeButton;
            GetChildProperty(axisProperty, "positiveButton").stringValue = axis.positiveButton;
            GetChildProperty(axisProperty, "altNegativeButton").stringValue = axis.altNegativeButton;
            GetChildProperty(axisProperty, "altPositiveButton").stringValue = axis.altPositiveButton;
            GetChildProperty(axisProperty, "gravity").floatValue = axis.gravity;
            GetChildProperty(axisProperty, "dead").floatValue = axis.dead;
            GetChildProperty(axisProperty, "sensitivity").floatValue = axis.sensitivity;
            GetChildProperty(axisProperty, "snap").boolValue = axis.snap;
            GetChildProperty(axisProperty, "invert").boolValue = axis.invert;
            GetChildProperty(axisProperty, "type").intValue = (int)axis.type;
            GetChildProperty(axisProperty, "axis").intValue = axis.axis - 1;
            GetChildProperty(axisProperty, "joyNum").intValue = axis.joyNum;

            serializedObject.ApplyModifiedProperties();
        }
        public static void SetupInputManager()
        {
            // Add mouse definitions
            AddAxis(new InputAxis() { name = "Mouse X", sensitivity = 1f, type = AxisType.MouseMovement, axis = 1 });
            AddAxis(new InputAxis() { name = "Mouse Y", sensitivity = 1f, type = AxisType.MouseMovement, axis = 2 });
            AddAxis(new InputAxis() { name = "Mouse ScrollWheel", sensitivity = 1f, type = AxisType.MouseMovement, axis = 3 });
        }
    */
    public static Dictionary<TKey, KeyCode> ClonarLista<TKey, KeyCode>
       (Dictionary<TKey, KeyCode> original)
    {
        Dictionary<TKey, KeyCode> ret = new Dictionary<TKey, KeyCode>(original.Count,
                                                                original.Comparer);
        foreach (KeyValuePair<TKey, KeyCode> entry in original)
        {
            ret.Add(entry.Key, (KeyCode)entry.Value);

        }
        return ret;
    }
    public static void TranscreverControles()
    {
        Dictionary<string, KeyCode> t = ClonarLista(Game_Player.instancia.Teclas);
        /*
          Dictionary<string, string> v = new Dictionary<string, string>();
         foreach (var tecla in t)
         {
             v.Add(tecla.Key, tecla.Value.ToString().ToLower().Replace("arrow", ""));
         }
         */
        foreach (var tecla in t)
        {
            PlayerPrefs.SetString(tecla.Key, tecla.Value.ToString());
        }

    }
}
