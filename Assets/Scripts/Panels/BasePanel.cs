using System;
using UnityEngine;
using UnityEngine.UI;

public class BasePanel : MonoBehaviour
{
    [SerializeField] private Selectable _onEnableSelectedItem;

    public virtual void OnEnable()
    {
        _onEnableSelectedItem?.Select();
    }
}