﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell : MonoBehaviour {
    public HexCoordinates Coordinates;

    [SerializeField]
    private int x, z;

    public int X
    {
        get
        {
            return x;
        }
    }
    public int Z
    {
        get
        {
            return z;
        }
    }

    public HexCoordinates(int x, int z)
    {
        this.x = x;
        this.z = z;
    }
}
