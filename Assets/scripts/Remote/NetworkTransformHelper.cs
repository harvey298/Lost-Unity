using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;
using Unity.Netcode.Components;

public class NetworkTransformHelper : NetworkTransform
{

    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }
}
