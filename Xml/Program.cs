using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace Xml
{
    class Program
    {
        static void Main(string[] args)
        {
            //var s = new SerializedClass
            //{
            //    intvalue = 1,
            //    boolvalue = false,
            //    child = new List<ChildClass>
            //    {
            //        new ChildClass
            //        {
            //            childint = 1,
            //            childstr = "c1"
            //        },
            //        new ChildClass
            //        {
            //            childint = 2,
            //            childstr = "c2"
            //        }
            //    }
            //};

            var s = new SerializedClass
            {
                intvalue = 1,
                boolvalue = true,
                prm = new List<PrmTbl>
                {
                    new PrmTbl
                    {
                        axis_num = 0,
                        prm_data = 0x0010,
                        prm_num = 0x0011
                    },
                    new PrmTbl
                    {
                        axis_num = 0,
                        prm_data = 0x0011,
                        prm_num = 0x0012
                    }
                }
            };

            var add = @".\test.xml";

            var ser = new XmlSerializer(typeof(SerializedClass));
            using (StreamWriter sw = new StreamWriter(add, false, Encoding.UTF8))
            {
                ser.Serialize(sw, s);
            }

            var ser2 = new XmlSerializer(typeof(SerializedClass));
            var cfg = new XmlReaderSettings()
            {
                CheckCharacters = false
            };
            SerializedClass s2;
            using (StreamReader sr = new StreamReader(add, Encoding.UTF8))
            using (XmlReader xr = XmlReader.Create(sr, cfg))
            {
                s2 = (SerializedClass)ser2.Deserialize(xr);
            }
            Console.WriteLine(s2.boolvalue);
            Console.ReadKey();
        }
    }
}
