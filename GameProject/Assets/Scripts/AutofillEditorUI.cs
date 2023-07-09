using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutofillEditorUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Button clear;
    public Button play;
    public Button reset;
    private ObjectResetter resetter;
    private LevelEditor editor;

    void Start()
    {
        resetter = FindObjectOfType<ObjectResetter>();
        editor = FindObjectOfType<LevelEditor>();
        if (editor)
        {
            clear.onClick.AddListener(editor.AllToInactive);
        }

        if(resetter)
        {
            play.onClick.AddListener(resetter.OnPlay);
            reset.onClick.AddListener(resetter.OnReset);
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
