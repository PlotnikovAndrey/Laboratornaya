using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using System.IO;

namespace Lab1ISIS
{
    class Test
    {
        public static string testXML(string text)
        {
            XDocument doc = new XDocument();
            XElement textxml = new XElement("Text", text);
            doc.Add(textxml);
            return Convert.ToString(textxml);
        }
        [DllImport("kernel32", CharSet = CharSet.Auto)] // Подключаем kernel32.dll и описываем его функцию WritePrivateProfilesString
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);
        public static string testWrite(string Section, string Key, string Value)
        {
            string Path = "D:\\file.ini";
            WritePrivateProfileString(Section, Key, Value, Path);
            return Value;
        }

        public static int DeleteKey(string Key, string Section = null)
        {
            testWrite(Section, Key, null);
            int result = 1;
            return result;
        }

        public static int DeleteSection(string Section = null)
        {
            testWrite(Section, null, null);
            int result = 1;
            return result;
        }
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        static extern uint GetPrivateProfileSection(string lpAppName, IntPtr lpReturnedString, uint nSize, string lpFileName);
        public static bool GetPrivateProfileSection(string appName, string fileName, out string[] section)
        {
            section = null;

            if (!System.IO.File.Exists(fileName))
                return false;

            const uint MAX_BUFFER = 32767;

            IntPtr pReturnedString = Marshal.AllocCoTaskMem((int)MAX_BUFFER * sizeof(char));

            uint bytesReturned = GetPrivateProfileSection(appName, pReturnedString, MAX_BUFFER, fileName);

            if ((bytesReturned == MAX_BUFFER - 2) || (bytesReturned == 0))
            {
                Marshal.FreeCoTaskMem(pReturnedString);
                return false;
            }
            string returnedString = Marshal.PtrToStringAuto(pReturnedString, (int)(bytesReturned - 1));

            section = returnedString.Split('\0');

            Marshal.FreeCoTaskMem(pReturnedString);
            return true;
        }
        public static string ReadINI(string Section, string Key)
        {
            string Path = "D:\\file.ini";
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }

        public static bool KeyExists(string Section, string Key)
        {
            return ReadINI(Section, Key).Length > 0;
        }
        [DllImport("kernel32", CharSet = CharSet.Auto)] // Еще раз подключаем kernel32.dll, а теперь описываем функцию GetPrivateProfileString
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);
        public static string ReadToEnd()
        {
            using (StreamReader sr = new StreamReader("D:\\fileopen.txt"))
            {
                String ltext1 = sr.ReadToEnd();
                return ltext1;
            }
        }

        public static string ReadToEndplustext(int i)
        {
            using (StreamReader sr = new StreamReader("D:\\fileopen.txt"))
            {
                String ltext1 = "";
                if (i == 1)
                {
                    ltext1 = "Неудачно! " + sr.ReadToEnd();
                }
                if (i == 2)
                {
                    ltext1 = "Удачно! " + sr.ReadToEnd();
                }
                return ltext1;
            }
        }

        public static bool testDirectory(string IniPath)
        {
            string Path1;
            Path1 = new FileInfo(IniPath).FullName.ToString();
            if (Path1 == "D:\\file.ini")
                return true;
            else
                return false;
        }
    }
}
