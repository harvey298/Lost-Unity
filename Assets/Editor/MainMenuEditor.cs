using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuEditor : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("Window/UI Toolkit/MainMenuEditor")]
    public static void ShowExample()
    {
        MainMenuEditor wnd = GetWindow<MainMenuEditor>();
        wnd.titleContent = new GUIContent("MainMenuEditor");
    }

    public void CreateGUI()
    {
        Debug.Log(m_VisualTreeAsset.name);

        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElements following a tree hierarchy.
        Label label = new Label("These controls were created using C# code.");
        root.Add(label);

        Button button = new Button();
        button.name = "button3";
        button.text = "This is button3.";
        root.Add(button);

        Toggle toggle = new Toggle();
        toggle.name = "toggle3";
        toggle.label = "Number?";
        root.Add(toggle);

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/MyCustomEditor.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);

        Debug.Log("Creating GUI");
        SetupButtonHandler();
    }


    private void SetupButtonHandler()
    {
        VisualElement root = rootVisualElement;

        var buttons = root.Query<Button>();
        foreach (var button in buttons.ToList())
        {
            Debug.Log(button.name);
        }
    }

    private void RoomJoinHandler()
    {

    }

}

