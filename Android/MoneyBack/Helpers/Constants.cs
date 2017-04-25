using System.IO;
using Environment = System.Environment;

namespace MoneyBack.Helpers
{
    public class Constants
    {
        public static readonly string DbFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "moneyback.db");
    }
}