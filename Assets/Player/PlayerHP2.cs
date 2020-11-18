﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Photon.Pun;

namespace Player
{
    public class PlayerHP2 : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private int PN;
        [SerializeField]
        private GameObject PlayerBody;
        static private float PlayerHp2 = 100;
        static private float outOfAreaCount = 0f;

        static private bool Playerdie = false;
        static private bool PlayerGetDamageB = false;
        void Update()
        {
            PlayerGetDamageB = false;
            if (PN != PhotonScriptor.ConnectingScript.informPlayerID())
            {
                return;
            }
            if (TimeManager.MainPhaze.InformMainphaze() == false)
            {
                return;
            }
            
            if (redPanel.OutOfAreaInform2() == true)
            {
                outOfAreaCount += Time.deltaTime;
                if (outOfAreaCount >= 1)
                {
                    PlayerHp2 -= 4;
                    outOfAreaCount = 0;
                }
            }
            if (redPanel.OutOfAreaInform2() == false)
            {
                outOfAreaCount = 0f;
            }
            if (PlayerHp2 <= 0)
            {
                photonView.RPC(nameof(PlayerDead), RpcTarget.All);
            }
        }

        static public void PlayerGetDamage(float Damage)
        {
            PlayerGetDamageB = true;
            PlayerHp2 -= Damage;
            Debug.Log("get Damage");
        }

        static public float InformPlayerHP()
        {
            return PlayerHp2;
        }
        
        static public void ResetPlayerHP()
        {
            PlayerHp2 = 100;
            outOfAreaCount = 0f;
        }

        [PunRPC]
        void PlayerDead()
        {
            TimeManager.MainPhaze.ResetMainphaze();
            UI.WinorLose.IncrementBlue();
            Debug.Log("Died");
            PlayerHp2 = 100;
            GameManager.GameRoundManager.SetEndM();
        }

        static public bool InformPlayerGetDamage()
        {
            return PlayerGetDamageB;
        }
    }
}