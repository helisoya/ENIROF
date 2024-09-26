using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using WiimoteApi;
using System;

/*
public struct WiiState : IInputStateTypeInfo
{

    public FourCC format => new FourCC('W', 'I', 'I', 'D');


    [InputControl(name = "ButtonB", layout = "Button")]
    public bool buttonB;

    [InputControl(name = "ButtonA", layout = "Button")]
    public bool buttonA;


    [InputControl(name = "Pointer", layout = "Vector2")]
    public Vector2 pointer;
}



// InputControlLayoutAttribute attribute is only necessary if you want
// to override the default behavior that occurs when you register your Device
// as a layout.
// The most common use of InputControlLayoutAttribute is to direct the system
// to a custom "state struct" through the `stateType` property. See below for details.
#if UNITY_EDITOR
[InitializeOnLoad]
#endif
[InputControlLayout(displayName = "Wiimote", stateType = typeof(WiiState))]
public class WiiDevice : InputDevice, IInputUpdateCallbackReceiver
{
    public static WiiDevice current;
    private Wiimote wiimote;

    public ButtonControl buttonB { get; private set; }
    public ButtonControl buttonA { get; private set; }
    public Vector2Control pointer { get; private set; }

    public void OnUpdate()
    {
        if (wiimote == null)
        {
            WiimoteManager.FindWiimotes();
            if (WiimoteManager.Wiimotes.Count > 0)
            {
                wiimote = WiimoteManager.Wiimotes[0];
            }
        }

        float[] data = wiimote.Ir.GetPointingPosition();
        WiiState state = new()
        {
            buttonA = wiimote.Button.a,
            buttonB = wiimote.Button.b,
            pointer = new Vector2(data[0] * Screen.width, data[1] * Screen.height)
        };

        InputSystem.QueueStateEvent(this, state);
    }

    protected override void FinishSetup()
    {
        base.FinishSetup();

        wiimote = WiimoteManager.Wiimotes[0];
        wiimote.SetupIRCamera(IRDataType.BASIC);

        buttonB = GetChildControl<ButtonControl>("ButtonB");
        buttonA = GetChildControl<ButtonControl>("ButtonA");
        pointer = GetChildControl<Vector2Control>("Pointer");

        current = this;
    }



    [RuntimeInitializeOnLoadMethod]
    private static void InitializeInPlayer() { }

    static WiiDevice()
    {
        InputSystem.RegisterLayout<WiiDevice>();

        WiimoteManager.FindWiimotes();
        if (WiimoteManager.Wiimotes.Count > 0)
        {
            InputSystem.AddDevice<WiiDevice>();
        }
    }
}*/