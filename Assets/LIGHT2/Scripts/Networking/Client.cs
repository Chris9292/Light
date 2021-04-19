/*using System;
using System.Net.Sockets;
using System.Net;
using Windows.Networking.Sockets;
using Windows;

public class Receiver
{
    HostName serverIp = new HostName("192.168.0.109");
    string port = "1337";

    async void Client()
    {
        DatagramSocket socket = new DatagramSocket(); //creating socket
        socket.MessageReceived += Socket_MessageReceived; //attach receive event
        await socket.BindEndpointAsync(serverIp, port); //bind socket
    }

    async void Socket_MessageReceived(DatagramSocket sender,
    DatagramSocketMessageReceivedEventArgs args) //receive event
    {
        Stream streamIn = args.GetDataStream().AsStreamForRead();
        StreamReader reader = new StreamReader(streamIn);
        string msg = await reader.ReadLineAsync();
        Print("Message received: " + msg);
    }


    // Current Server application on HoloLens (using Windows.Networking.Sockets...)
*//*    IPAddress serverIp = IPAddress.Parse("192.168.0.109");
    const int port = 1337;

    async void Server()
    {
        var udpClient = new UdpClient();
        var serverEP = new IPEndPoint(serverIp, port);
        byte[] bytes = Encoding.ASCII.GetBytes("Hello from Client!"); //parse
        await udpClient.SendAsync(bytes, bytes.Length, serverEP); //send
    }*//*
}
*/