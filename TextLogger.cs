using System.Threading;
using NLog;
using opcode4.core.Model.Identity;
using opcode4.core.Model.Log;
using opcode4.utilities;

namespace opcode4.log
{
    public static class TextLogger
    {
        private const string Template = "[Id:{0}, Name:{1}] Message:\r\n{2}";
        private static readonly Logger Log = LogManager.GetLogger(ConfigUtils.ReadStringDef("TextLoggerName", "applogger"));
        
        public static void Trace(string msg)
        {
            try
            {
                Log.Trace(TryFillActor(msg));
            }
            catch { }
        }

        public static void Trace(LogEntity entity)
        {
            try
            {
                Log.Trace(Template, entity.ActorId, entity.ActorName, entity.Message);
            }
            catch { }
        }

        public static void Debug(string msg)
        {
            try
            {
                Log.Debug(TryFillActor(msg));
            }
            catch { }
        }

        public static void Debug(LogEntity entity)
        {
            try
            {
                Log.Trace(Template, entity.ActorId, entity.ActorName, entity.Message);
            }
            catch { }
        }

        public static void Info(string msg)
        {
            try
            {
                Log.Info(TryFillActor(msg));
            }
            catch { }
        }
        public static void Info(LogEntity entity)
        {
            try
            {
                Log.Trace(Template, entity.ActorId, entity.ActorName, entity.Message);
            }
            catch { }

        }

        public static void Warning(string msg)
        {
            try
            {
                Log.Warn(TryFillActor(msg));
            }
            catch { }
        }
        public static void Warning(LogEntity entity)
        {
            try
            {
                Log.Trace(Template, entity.ActorId, entity.ActorName, entity.Message);
            }
            catch { }
        }

        public static void Error(string msg)
        {
            try
            {
                Log.Error(TryFillActor(msg));
            }
            catch { }
        }
        public static void Error(LogEntity entity)
        {
            try
            {
                Log.Trace(Template, entity.ActorId, entity.ActorName, entity.Message);
            }
            catch { }

        }

        public static void Critical(string msg)
        {
            try
            {
                Log.Fatal(TryFillActor(msg));
            }
            catch { }
        }
        public static void Critical(LogEntity entity)
        {
            try
            {
                Log.Trace(Template, entity.ActorId, entity.ActorName, entity.Message);
            }
            catch { }
        }

        private static string TryFillActor(string msg)
        {
            var identity = Thread.CurrentPrincipal.Identity as CustomIdentity ?? new CustomIdentity();

            return string.Format(Template, identity.Id, identity.Name, msg);

        }
    }
}



