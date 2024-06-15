using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GoalNodeTypeElementUI : MonoBehaviour
{
    private NodeType nodeType;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI tmp_count;

    public void Setting(NodeType nodeType, Sprite sprite, string count)
    {
        this.nodeType = nodeType;
        image.sprite = sprite;
        tmp_count.text = count;
    }

    public void UpdateUI(int count)
    {
        tmp_count.text = count.ToString();
    }

    public NodeType GetNodeType() => nodeType;
}
