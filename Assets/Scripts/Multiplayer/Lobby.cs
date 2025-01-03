using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Networking.Transport;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{

    [SerializeField] private Text PlayersCountText;
    [SerializeField] private Text _joinCodeText;
    [SerializeField] private InputField _codeInputText;

    private string _joinCode = "n/a";
     private List<Region> _regions = new List<Region>();
     private Allocation _allocation;
     private JoinAllocation playerAllocation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
