using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class HeroCommandGenerator : MonoBehaviour
{
    [Header("Prefab")]
    public HeroCommand commandPrefab;

    [Header("Parameters")]
    public int commandCount = 60;

    [Header("Generate Enemy")]
    public bool e_populateCommands = false;

    [SerializeField]
    public List<HeroCommand> commandArray;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            if (e_populateCommands)
            {
                Initiate();
                e_populateCommands = false;
            }
        }
#endif
    }

    private void Initiate()
    {
#if UNITY_EDITOR

        // Clear everythin
        foreach (HeroCommand e in commandArray)
        {
            if (!e) continue;
            DestroyImmediate(e.gameObject);

        }
        commandArray.Clear();


        // Populate Enemy
        for (int i = 0; i < commandCount; i++)
        {
            HeroCommand e = PrefabUtility.InstantiatePrefab(commandPrefab, this.transform) as HeroCommand;

            e.gameObject.name = $"Command [{i}]";
            commandArray.Add(e);
        }
        EditorUtility.SetDirty(this);
#endif
    }
}
