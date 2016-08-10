using System;
using Commons.Core.Utilities;
using Vayosoft.Monitor.Client;
using Vayosoft.Monitor.Shared;

namespace Commons.Log.Snmp
{
    public static class SnmpHelper
    {
        public static void SendTrap(TrapType trapType, SeverityType severity, string msg)
        {
            if (!ConfigUtils.SafeReadBool("UseSNMP"))
                return;
            try
            {
                var client = SnmpClientWcf.GetInstance;
                client.Bind(ConfigUtils.ReadString("WCF.VayoMonitorService"));
                var appName = System.Diagnostics.Process.GetCurrentProcess().ProcessName; //System.IO.Path.GetFileNameWithoutExtension(Environment.GetCommandLineArgs()[0]);
                if (client.SendTrap(appName, trapType, severity, msg, (int)TrapService.WiFiOffload))
                    TextLogger.Info("Trap sent: " + msg);
            }
            catch (Exception ex)
            {
                TextLogger.Error("SnmpHelper.SendTrap: " + ex.Message);
            }
        }

        public static void SendTrap(string appObj, TrapType trapType, SeverityType severity, string msg)
        {
            if (!ConfigUtils.SafeReadBool("UseSNMP"))
                return;
            try
            {
                var client = SnmpClientWcf.GetInstance;
                client.Bind(ConfigUtils.ReadString("WCF.VayoMonitorService"));
                if (client.SendTrap(appObj, trapType, severity, msg, (int)TrapService.AppStorage))
                    TextLogger.Info("Trap sent: " + msg);
            }
            catch (Exception ex)
            {
                TextLogger.Error("SnmpHelper.SendTrap: " + ex.Message);
            }
        }
    }
}
