using System;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    private Panel currentPanel;

    private Stack<Panel> panels;

    [SerializeField] 
    private Panel firstPanel;

    private void Awake()
    {
        currentPanel = firstPanel;
    }

    public void NextPanel()
    {
        panels.Push(currentPanel);
        currentPanel = currentPanel.nextPanel;
        currentPanel.Show();
    }

    public void PrevPanel()
    {
        currentPanel.Hide();
        var panel = panels.Pop();
        panel.Show();
    }
}
