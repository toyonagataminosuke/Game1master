﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEditor;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;

namespace PhotonScriptor
{
    public class ConnectingScript : MonoBehaviourPunCallbacks
    {
        static private string RoomName = "room";
        static private int RoomNo = 0;
        static private int PlayerID = 2;
        private float CountPlayers = 0;

        static private bool SetNoComplete = false;
        static private bool Connected = false;
        static private bool startGame = false;
        static private bool FailedtoRoom = false;
        Photon.Realtime.Player Photonplayer;
        static RoomOptions RoomOPS = new RoomOptions()
        {
            MaxPlayers = 2, 
            IsOpen = true,
            IsVisible = true,
        };
        
        void Start()
        {
            LeaveGame();
            RoomNo = 0;
            PlayerID = 0;
            CountPlayers = 0;
            SetNoComplete = false;
            startGame = false;
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.SerializationRate = 60;
            PhotonNetwork.SendRate = 30;  
        }

        private void Update()
        {
            if(PhotonNetwork.IsConnected == false)
            {
                PhotonNetwork.JoinLobby();
            }
            //Debug.Log(CountPlayers);
            if(Connected == true && SetNoComplete == true && PhotonNetwork.IsConnectedAndReady == true)
            {
                PhotonNetwork.JoinOrCreateRoom(RoomName + RoomNo, RoomOPS, TypedLobby.Default);
                Connected = false;
            }
            if (CountPlayers != 2)
            {
                CountPlayers = 0;
                foreach (var i in PhotonNetwork.PlayerList)
                {
                    CountPlayers++;
                }
            }
            if (CountPlayers == 2 && startGame == false)
            {
                startGame = true;
                Debug.Log("countPlayer 2");
                TimeManager.BluePrint.BruePrintPhaze.StartBluePrintM();
            }
        }
        
        public override void OnConnectedToMaster()
        {
            Connected = true;
        }

        public override void OnJoinedRoom()
        {
            Photonplayer = PhotonNetwork.LocalPlayer;
            PlayerID = Photonplayer.ActorNumber; 
            //PlayerID = 2;//プレイヤー2としてプレイするよう
            Debug.Log("PlayerID = " + PlayerID);
            Debug.Log("Succesfully Connected");
            //TimeManager.BluePrint.BruePrintPhaze.StartBluePrintM();//テスト用
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            base.OnJoinRoomFailed(returnCode, message);
            FailedtoRoom = true;
            SceneManager.LoadScene(1);
        }
        public override void OnLeftRoom()
        {
            Debug.Log("leaveRoom");
        }

        public override void OnLeftLobby()
        {
            Debug.Log("Leave Lobby");
        }

        static public int informPlayerID()
        {
            return PlayerID;
        }

        static public bool informConnected()
        {
            return Connected;
        }

        static public void SetRoomNo(int No)
        {
            RoomNo = No;
            SetNoComplete = true;
        }

        static public bool informSetNoComplete()
        {
            return SetNoComplete;
        }

        static public bool informStartGame()
        {
            return startGame;
        }

        static public void LeaveGame()
        {
            //TimeManager.MainPhaze.ResetMainphaze();
            Connected = true;
            PhotonNetwork.LeaveRoom();
            //PhotonNetwork.LeaveLobby();
            Debug.Log("LEAVE GAME");
        }

        static public bool informFailed()
        {
            return FailedtoRoom;
        }

        static public void FailedfalseM()
        {
            FailedtoRoom = false;
        }
    }
}