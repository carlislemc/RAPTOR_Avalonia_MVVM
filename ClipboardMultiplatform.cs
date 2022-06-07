using Avalonia;
using Avalonia.Input;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace raptor
{
    class ClipboardMultiplatform
    {

        private static DataObject? clipboard_data;
        public static void SetDataObject(object data, bool afterExit)
        {
            DataObject dataObject = new DataObject();
            dataObject.Set("raptor-data", data);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                clipboard_data = dataObject;
            }
            else
            {
                Application.Current.Clipboard.SetDataObjectAsync(dataObject);
            }
        }
        public static async Task<object> GetDataObjectAsync()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return clipboard_data.Get("raptor-data");
            }
            else
            {
                return await Application.Current.Clipboard.GetDataAsync("raptor-data");
            }
        }
    }
}
