using System;
using opcode4.core.Exceptions;
using opcode4.core.Helpers;
using opcode4.core.Model.Log;
using opcode4.utilities;

namespace opcode4.log
{
    public static class DBLogger
    {
        private static readonly MultiValueParameter _LogWriterClass = ConfigUtils.ReadSaveMultiValueParameter("LOG.WRITER");

        public static string AddEvent(LogEntity entity)
        {
            try
            {
                if (!IdentityUtils.ShouldLog(entity.EventType))
                    return null;

                if (string.IsNullOrEmpty(entity.ActorName))
                {
                    var identity = IdentityUtils.CurrentIdentity;
                    if (identity != null)
                    {
                        entity.ActorId = identity.Id;
                        entity.ActorName = identity.Name;
                    }
                    else
                    {
                        entity.ActorName = "NOT_AVAILABLE";
                    }
                }

                if (string.IsNullOrEmpty(entity.FormalMessage))
                    entity.FormalMessage = _LogWriterClass == null ? "UNSPECIFIED" : null;

                entity.ServerId = ConfigUtils.ServerId;

                if (entity.EventDate.Year < 2000)
                    entity.EventDate = DateTime.Now;

               return _LogWriterClass == null 
                    ? new DbLogWriter().AddEvent(entity)
                    :((ILogWriter)Activator.CreateInstance(_LogWriterClass.Items[0], _LogWriterClass.Items[1]).Unwrap()).AddEvent(entity);
            }
            catch (Exception ex)
            {
                TextLogger.Error("Error writing to Db: " + ex);
                TextLogger.Error(entity);
                return null;
            }
        }


        public static string Error(string msg, params object[] args)
        {
            return AddEvent(new LogEntity
                {
                    EventType = LogEventType.Error,
                    Message = args.Length == 0 ? msg : string.Format(msg, args),
                });
        }

        public static string Error(long? actorId, string actorName, string actorType, string msg, params object[] args)
        {
            return AddEvent(new LogEntity
            {
                ActorId = actorId,
                ActorName = actorName,
                EventType = LogEventType.Error,
                Message = args.Length == 0 ? msg : string.Format(msg, args)
            });
        }

        public static string Error(CustomException exp)
        {
            return AddEvent(new LogEntity
            {
                EventType = LogEventType.Error,
                FormalMessage = exp.ToFormalString(),
                Message = exp.Message
            });
        }


        public static string Critical(LogTarget target, string msg, params object[] args)
        {
            return AddEvent(new LogEntity
            {
                EventType = LogEventType.CriticalError,
                LogTarget = target,
                Message = args.Length == 0 ? msg : string.Format(msg, args),
            });
        }

        public static string Critical(LogTarget target, long? actorId, string actorName, string actorType, string msg, params object[] args)
        {
            return AddEvent(new LogEntity
            {
                ActorId = actorId,
                ActorName = actorName,
                EventType = LogEventType.CriticalError,
                LogTarget = target,
                Message = args.Length == 0 ? msg : string.Format(msg, args)
            });
        }

        public static string Critical(LogTarget target, CustomException exp)
        {
            return AddEvent(new LogEntity
            {
                EventType = LogEventType.CriticalError,
                LogTarget = target,
                FormalMessage = exp.ToFormalString(),
                Message = exp.Message
            });
        }


        public static string Info(string msg, params object[] args)
        {
            return AddEvent(new LogEntity
            {
                EventType = LogEventType.Info,
                Message = args.Length == 0 ? msg : string.Format(msg, args),
            });
        }

        public static string Info(long? actorId, string actorName, string actorType, string msg, params object[] args)
        {
            return AddEvent(new LogEntity
            {
                ActorId = actorId,
                ActorName = actorName,
                EventType = LogEventType.Info,
                Message = args.Length == 0 ? msg : string.Format(msg, args)
            });
        }

        public static string Info(CustomException exp)
        {
            return AddEvent(new LogEntity
            {
                EventType = LogEventType.Info,
                FormalMessage = exp.ToFormalString(),
                Message = exp.Message
            });
        }


        public static string Debug(string msg, params object[] args)
        {
            return AddEvent(new LogEntity
            {
                EventType = LogEventType.Debug,
                Message = args.Length == 0 ? msg : string.Format(msg, args),
            });
        }

        public static string Debug(long? actorId, string actorName, string actorType, string msg, params object[] args)
        {
            return AddEvent(new LogEntity
            {
                ActorId = actorId,
                ActorName = actorName,
                EventType = LogEventType.Debug,
                Message = args.Length == 0 ? msg : string.Format(msg, args)
            });
        }

        public static string Debug(CustomException exp)
        {
            return AddEvent(new LogEntity
            {
                EventType = LogEventType.Debug,
                FormalMessage = exp.ToFormalString(),
                Message = exp.Message
            });
        }


        public static string Warning(string msg, params object[] args)
        {
            return AddEvent(new LogEntity
            {
                EventType = LogEventType.Warning,
                Message = args.Length == 0 ? msg : string.Format(msg, args),
            });
        }

        public static string Warning(long? actorId, string actorName, string actorType, string msg, params object[] args)
        {
            return AddEvent(new LogEntity
            {
                ActorId = actorId,
                ActorName = actorName,
                EventType = LogEventType.Debug,
                Message = args.Length == 0 ? msg : string.Format(msg, args)
            });
        }

        public static string Warning(CustomException exp)
        {
            return AddEvent(new LogEntity
            {
                EventType = LogEventType.Warning,
                FormalMessage = exp.ToFormalString(),
                Message = exp.Message
            });
        }

    }
}
