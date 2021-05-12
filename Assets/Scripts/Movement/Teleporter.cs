using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    Transform teleportTo;
    public Transform TeleportTo { get { return teleportTo; } private set { TeleportTo = value; } }

    private void Start()
    {
        TeleportTo = teleportTo;
    }
}
