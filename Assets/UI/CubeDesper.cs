﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeDesper : MonoBehaviour
{
    [SerializeField]
    private Text CubeText;

    private int cube;
    private int limit;

    void Update()
    {
        cube = Cube.CubeSet.CubeLeftInformer();
        limit = Cube.CubeSet.InformLimit();
        if (cube < 0)
        {
            cube = 0;
        }
        CubeText.text ="CREATE\nWALL" + cube + "/" + limit;
    }
}
