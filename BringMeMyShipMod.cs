﻿using OWML.ModHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static LoadManager;

namespace BringMeMyShip
{
    public class BringMeMyShipMod : ModBehaviour
    {

		private readonly float offsetDistance = 10f;

		private readonly Dictionary<string, string> Strings = new Dictionary<string, string>()
		{
			{"ship", "Ship_Body"},
			{"player", "Player_Body" }
		};

		private Dictionary<string, Key> KeyBinds = new Dictionary<string, Key>()
		{
			{ "activate", Key.P }
		};

        private GameObject player_body;
        private GameObject ship_body;

        private void Start()
        {
			ModHelper.Console.WriteLine($"In {nameof(BringMeMyShipMod)}!");
		}

		private void Update()
		{
			if (GetKeyDown(KeyBinds["activate"]))
			{
				if (player_body == null || ship_body == null) return;
				DoShipTeleport();
			}
		}

		private void DoShipTeleport()
		{
			Vector3 newPos = player_body.transform.position;
			Vector3 offset = player_body.transform.up * offsetDistance;
			newPos += offset;

			OWRigidbody s_rb = ship_body.GetComponent<OWRigidbody>();
			OWRigidbody p_rb = player_body.GetComponent<OWRigidbody>();

			ModHelper.Console.WriteLine("Here's your ship");
			s_rb.SetPosition(newPos);
			s_rb.SetRotation(player_body.transform.rotation);
			s_rb.SetVelocity(p_rb.GetVelocity());

			
		}

		private void LateUpdate()
		{
            if (player_body == null) player_body = GetPlayerBody();
            if (ship_body == null) ship_body = GetShipBody();
		}

		private GameObject GetShipBody()
		{
			return GameObject.Find(Strings["ship"]);
		}

		private GameObject GetPlayerBody()
		{
			return GameObject.Find(Strings["player"]);
		}

		public void OnSceneLoad()
		{

		}

		private bool GetKeyDown(Key keyCode)
		{
			return Keyboard.current[keyCode].wasPressedThisFrame;
		}

	}
}
