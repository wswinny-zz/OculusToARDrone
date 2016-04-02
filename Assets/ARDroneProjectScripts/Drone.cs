using UnityEngine;
using System.Collections;

using AR.Drone.Client;
using AR.Drone.Video;
using AR.Drone.Data;
using AR.Drone.Data.Navigation;
using FFmpeg.AutoGen;
using NativeWifi;

namespace Oculus2ARDrone
{
    public class Drone
    {
        public Drone()
        {

            GlobalVariables.videoData = new byte[GlobalVariables.width * GlobalVariables.height * 3];

            GlobalVariables.videoPacketDecoderWorker = new VideoPacketDecoderWorker(PixelFormat.BGR24, true, OnVideoPacketDecoded);
            GlobalVariables.videoPacketDecoderWorker.Start();

            GlobalVariables.droneClient = new DroneClient("192.168.1.1");
            GlobalVariables.droneClient.UnhandledException += HandleUnhandledException;
            GlobalVariables.droneClient.VideoPacketAcquired += OnVideoPacketAcquired;
            GlobalVariables.droneClient.NavigationDataAcquired += navData => GlobalVariables.navigationData = navData;

            GlobalVariables.videoPacketDecoderWorker.UnhandledException += HandleUnhandledException;
            GlobalVariables.droneClient.Start();

            switchDroneCamera(AR.Drone.Client.Configuration.VideoChannelType.Horizontal);

            GlobalVariables.wlanClient = new WlanClient();
        }

        public bool takeoff()
        {
            GlobalVariables.droneClient.Takeoff();
            return true;
        }

        public bool land()
        {
            GlobalVariables.droneClient.Land();
            return true;
        }

        public bool enterHoverMode()
        {
            GlobalVariables.droneClient.Progress(AR.Drone.Client.Command.FlightMode.Hover, pitch: 0, roll: 0, gaz: 0, yaw: 0);
            return true;
        }

        public void move(Orientation oriantation)
        {
            GlobalVariables.droneClient.Progress(AR.Drone.Client.Command.FlightMode.Progressive, pitch: oriantation.Pitch, roll: oriantation.Roll, gaz: oriantation.Gaz, yaw: oriantation.Yaw);
        }

        private void switchDroneCamera(AR.Drone.Client.Configuration.VideoChannelType Type)
        {
            var configuration = new AR.Drone.Client.Configuration.Settings();
            configuration.Video.Channel = Type;
            GlobalVariables.droneClient.Send(configuration);
        }

        private void OnVideoPacketAcquired(VideoPacket packet)
        {
            if (GlobalVariables.videoPacketDecoderWorker.IsAlive)
            {
                Debug.Log("Hi I'm alive!!!!!");
                GlobalVariables.videoPacketDecoderWorker.EnqueuePacket(packet);
            }
        }
        
        private void OnVideoPacketDecoded(VideoFrame frame)
        {
            Debug.Log("Video Data is here!!");
            GlobalVariables.videoData = frame.Data;
        }

        void HandleUnhandledException(object arg1, System.Exception arg2)
        {
            Debug.Log(arg2);
        }
    }
}
