using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Xml
{
    public class SerializedClass
    {
        public int intvalue { get; set; }
        public bool boolvalue { get; set; }

        [XmlIgnore]
        public List<PrmTbl> prm { get; set; }


        public List<PrmTblHex> prmhex
        {
            get
            {
                return ConvertToHex();
            }
            set
            {
                ConvertToDec(value);
            }
        }
        public SerializedClass()
        {
        }

        void ConvertToDec(List<PrmTblHex> hexList)
        {
            prm = new List<PrmTbl>();
            foreach(var hex in hexList)
            {
                var p = new PrmTbl();
                p.axis_num = hex.axis_num;
                p.prm_data = short.Parse(hex.prm_data);
                p.prm_num = short.Parse(hex.prm_num);
                
                prm.Add(p);
            }
        }

        List<PrmTblHex> ConvertToHex()
        {
            var pxl = new List<PrmTblHex>();
            if (prm == null) prm = new List<PrmTbl>();
            foreach(var p in prm)
            {
                var px = new PrmTblHex();

                px.axis_num = p.axis_num;
                px.prm_num = p.prm_num.ToString("x");
                px.prm_data = p.prm_data.ToString("x");
                pxl.Add(px);
            }

            return pxl;
        }
    }

    public class PrmTbl
    {
        public int axis_num { get; set; }                    /* axis number [ 0: system, 1-32: axis ]							*/
        public short prm_num { get; set; }                   /* parameter number													*/
        public short prm_data { get; set; }                  /* parameter data													*/

        public PrmTbl()
        {
        }
    }

    public class PrmTblHex
    {
        public int axis_num;                    /* axis number [ 0: system, 1-32: axis ]							*/
        public string prm_num;                   /* parameter number													*/
        public string prm_data;                  /* parameter data													*/

        public PrmTblHex()
        {
        }
    }

}
