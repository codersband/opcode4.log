using System.Globalization;
using opcode4.core;
using opcode4.core.Helpers;
using opcode4.core.Model.Log;

namespace opcode.log
{
    public class DbLogWriter: ILogWriter
    {
        public string AddEvent(LogEntity entity)
        {
            //#warning IocFactory must be changed to activator or moved to specified assembly (Commons.MySql and ect)

            entity.HasDetails = !string.IsNullOrEmpty(entity.Message) && entity.Message.Length > 512;
            IoCFactory.DAO.Using(repository => repository.Set(entity));
            return entity.LID.ToString(CultureInfo.InvariantCulture);
        }
    }
}
