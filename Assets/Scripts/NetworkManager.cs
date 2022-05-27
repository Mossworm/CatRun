using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//network 
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using YCCSNET;



public class NetworkManager : MonoBehaviour
{
    GameObject player;
    static Socket clntSocket;
    static EndPoint serverEP;

    static void send<T>(T data) where T : packet_t<T> {
        var buf = packet_mgr.make_buffer<T>(data.Serialize());
        clntSocket.SendTo(buf.ToArray(), serverEP);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("Player");
        clntSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        serverEP = new IPEndPoint(IPAddress.Loopback, 10200);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendData(int dir) {
        var input = new p_input();
        input.input = (char)dir;
        send(input);
    }
}
