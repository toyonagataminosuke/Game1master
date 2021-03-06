﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeManager;
namespace Cube
{
    public class CubeManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject CreativeCube;
        [SerializeField]
        private GameObject CreativeCube2;
        static private bool Cubestate = true;

        static private bool CubIncreased = false;
        private void Start()
        {
            CubIncreased = false;

            CreativeCube.SetActive(false);
            CreativeCube2.SetActive(false);
        }

        void Update()
        {
            if (PreParationTime.InformPreparationState() == false)
            {
                if (Cubestate == true)
                {
                    CreativeCube.SetActive(false);
                    CreativeCube2.SetActive(false);
                    Cubestate = false;
                    CubIncreased = false;
                    return;
                }
            }
            if (PreParationTime.InformPreparationState() == true)
            {
                if (Cubestate == false)
                {
                    CreativeCube.SetActive(true);
                    CreativeCube2.SetActive(true);
                    Cubestate = true;
                    if (CubIncreased == true)
                    {
                        CubeSet.IncreaseCube();
                    }
                    return;
                }
            }
        }

        static public void IncreaseCube()
        {
            CubIncreased = true;
        }
    }
}
