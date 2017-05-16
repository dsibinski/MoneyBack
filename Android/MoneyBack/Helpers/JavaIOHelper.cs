using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;

namespace MoneyBack.Helpers
{
    public class JavaIoHelper
    {
        public static void RemoveJavaFileIfExists(string filePath)
        {
            var file = new File(filePath);
            if (!file.Exists()) return;
            if (!file.Delete())
                throw new ArgumentException($"File {filePath} couldn't be deleted!");
        }
    }
}