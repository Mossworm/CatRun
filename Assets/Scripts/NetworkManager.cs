using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//network 
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using YCCSNET;
using System.Linq;
using System.Threading.Tasks;

public class NetworkManager : MonoBehaviour
{
    public static UdpClient clnt = new UdpClient();

    public static string server_ip = "127.0.0.1";
    public static int server_port = 9100;

    public static int seed = 0;
    public static char id;

    public static void send<T>(T data) where T : packet_t<T> {
        var buf = packet_mgr.make_buffer<T>(data.Serialize());
        clnt.Send(buf.ToArray(), buf.Count, server_ip, server_port);
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        Task task;
        task = new Task(() => {
            while (true) {
                packet_mgr.packet_read(recv());
            }
        });
        task.Start();

        clnt.Send(new byte[1] { 0 }, 1, server_ip, server_port);
        clnt.Send(new byte[2] { 0, 0 }, 2, server_ip, server_port);
    }

    private void Awake() {
    }

    public static List<byte> recv() {
        IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
        return clnt.Receive(ref remoteIPEndPoint).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
