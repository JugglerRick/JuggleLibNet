using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuggleLibNet
{
    class DropReport
    {
        public enum DropType : int
        {
            CATCH_MISSED = 0,
            MID_AIR_COLLISION,
            HAND_FULL,
            HAND_DROPPED,
            DROP
        };

        DropReport(DropType type, CProp prop)
        {
            ReportType = type;
            AddProp(prop);
        }

        DropReport(DropType type, CProp prop, CHand hand) : this(type, prop)
        {
            Hand = hand;
        }

        public DropType ReportType { get; set; }

        CProp GetProp(int id)
        {
            return _props.Find((p) => { return p.Id == id; });
        }

        void AddProp(CProp prop) { _props.Add(prop); }


        IEnumerator<CProp> GetProps()
        {
            return _props.GetEnumerator();
        }


        
        public CHand Hand { get; set; }


        protected List<CProp> _props;

    }
}
