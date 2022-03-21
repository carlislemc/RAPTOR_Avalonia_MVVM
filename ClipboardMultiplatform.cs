using Avalonia;
using Avalonia.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace raptor
{
    class ClipboardMultiplatform
    {

        private static DataObject clipboard_data;
        public static void SetDataObject(object data, bool afterExit)
        {
            DataObject dataObject = new DataObject();
            dataObject.Set("raptor-data", data);
            if (Component.MONO)
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
            if (Component.MONO)
            {
                return clipboard_data;
            }
            else
            {
                return await Application.Current.Clipboard.GetDataAsync("raptor-data");
            }
        }
    }
}
