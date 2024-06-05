using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class KeyboardToggle : MonoBehaviour
{
    public OVRVirtualKeyboard virtualKeyboard;

    public void ToggleKeyboard()
    {
        if (virtualKeyboard != null)
        {
            // Toggle the enabled state of the keyboard component
            virtualKeyboard.enabled = !virtualKeyboard.enabled;
        }
    }
}
