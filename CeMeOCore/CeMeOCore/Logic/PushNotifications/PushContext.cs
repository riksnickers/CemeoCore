using CeMeOCore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CeMeOCore.Logic.PushNotifications
{
    public class PushContext : IPushPlatform
    {
        private PushPlatform _pushPlatform;

        public PushContext()
        {
            this._pushPlatform = null;
        }

        public void Send(Device device, string message)
        {
            switch( device.Platform )
            {
                case Platform.Apple:
                    this._pushPlatform = new PushApple();
                    break;
                case Platform.Android:
                    this._pushPlatform = new PushAndroid();
                    break;
                default:
                    this._pushPlatform = null;
                    break;
            }

            if( this._pushPlatform != null )
            {
                _pushPlatform.Send(device, message);
            }
        }
    }
}