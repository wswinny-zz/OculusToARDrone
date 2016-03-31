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
    public static class GlobalVariables
    {
        public static float battery = 0;

        public static string navigationString = "";

        public static byte[] videoData;
        public static int width = 640;
        public static int height = 360;

        public static VideoPacketDecoderWorker videoPacketDecoderWorker;
        public static DroneClient droneClient;
        public static NavigationData navigationData;
    }
}
