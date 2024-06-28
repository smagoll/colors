using System;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public static Action switchPanel;
    
    private Panel currentPanel;

    private Stack<Panel> panels;

    [SerializeField] 
    private Panel firstPanel;

    private void Awake()
    {
        currentPanel = firstPanel;
        panels = new();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PrevPanel();
        }
    }

    public void NextPanel()
    {
        if (currentPanel.nextPanel == null) return;
        currentPanel.Hide();
        panels.Push(currentPanel);
        currentPanel = currentPanel.nextPanel;
        currentPanel.Show();
    }

    public void PrevPanel()
    {
        if (panels.Count == 0) return;
        currentPanel.Hide();
        var panel = panels.Pop();
        currentPanel = panel;
        currentPanel.Show();
    }

    private void OnEnable()
    {
        switchPanel += NextPanel;
    }
    
    private void OnDisable()
    {
        switchPanel -= NextPanel;
    }
}
