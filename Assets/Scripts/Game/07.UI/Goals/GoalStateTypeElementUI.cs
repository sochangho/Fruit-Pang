using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoalStateTypeElementUI : MonoBehaviour
{
    private NodeState nodeState;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI tmp_count;

    public void Setting(NodeState nodeState, Sprite sprite,string count)
    {
        this.nodeState = nodeState;
        image.sprite = sprite;
        tmp_count.text = count;
    }

    public void UpdateUI(int count)
    {
        tmp_count.text = count.ToString();
    }

    public NodeState GetNodeState() => nodeState;
}
