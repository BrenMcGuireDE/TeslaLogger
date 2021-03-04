﻿using System;
using System.Data;
using System.Linq;
using System.Reflection;

namespace TeslaLogger
{
    public abstract class StaticMapProvider
    {

        public enum MapMode
        {
            Regular,
            Dark
        }

        private static StaticMapProvider _StaticMapProvider = null;

        protected StaticMapProvider()
        {
        }

        public static StaticMapProvider GetInstance()
        {
            if (_StaticMapProvider == null)
            {
                foreach (Type type in Assembly.GetAssembly(typeof(StaticMapProvider)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(StaticMapProvider))))
                {
                    Logfile.Log("available MapProvider: " + type);
                }
            }
            return _StaticMapProvider;
        }

        public abstract void CreateChargingMap(double lat, double lng, int width, int height, MapMode mapmode, string filename);
        public abstract void CreateParkingMap(double lat, double lng, int width, int height, MapMode mapmode, string filename);
        public abstract void CreateTripMap(DataTable coords, int width, int height, MapMode mapmode, string filename);
    }
}
