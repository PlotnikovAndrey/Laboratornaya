using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NUnit.Framework;

namespace Lab1ISIS
{
    [TestFixture]
    class NUnitTests
    {
        [Test]
        public void Test_testXML()
        {
            string real;
            real = Test.testXML("лабораторная работа");
            Assert.AreEqual("<Text>лабораторная работа</Text>", real); // тест на соответсвии форме записи
        }

        [Test]
        public void Test_iniWrite()
        {
            string read = "";
            string writer;
            writer = Test.testWrite("Section1", "Key1", "test");
            foreach (string x in File.ReadAllLines("D:\\file.ini"))
            {
                string[] data = x.Split('=');
                if (data.Length == 2 && data[0] == "KeyOne")
                {
                    read = data[1]; // Вывод: test
                    break;
                }
            }
            Assert.AreEqual(writer, read); //тест на проверку корректность записи в ini, что записали в ключ, то и должны прочитать!
        }

        [Test]
        public void Test_delete_key()
        {
            int result = Test.DeleteKey(null);
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void Test_delete_section()
        {
            int result = Test.DeleteSection();
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void Test_getprivatsection()
        {
            string[] a = new string[] { };
            bool result = Test.GetPrivateProfileSection(null, null, out a);
            Assert.That(result, Is.False);
        }
        [Test]
        public void Test_key()
        {
            bool result = Test.KeyExists(null, null);
            Assert.That(result, Is.True);
        }
        [Test]
        public void Read_ini_test()
        {
            string result = Test.ReadINI("Section1", "Key1");
            Assert.That(result, Is.EqualTo("test"));
        }

        [Test]
        public void Read_text_test()
        {
            string text = "";
            string result = Test.ReadToEnd();
            foreach (string x in File.ReadAllLines("D:\\fileopen.txt"))
            {
                text = x;
            }
            Assert.AreEqual(text, result);
        }

        [Test]
        public void Read_text1_test()
        {
            string result = Test.ReadToEndplustext(1);
            Assert.That(result, Is.EqualTo("Неудачно! лабораторная работа"));
        }

        [Test]
        public void Directory_test()
        {
            bool result = Test.testDirectory("D:\\fileFalse.ini");
            Assert.That(result, Is.False);
        }
    }
}
