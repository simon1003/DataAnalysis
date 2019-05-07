using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataAnalysis.Framework.Common.Json
{
    [DataContract]
    public class JsonResult
    {
        private bool _resultFlag = true;
        private object _dataSource;

        public JsonResult(object data)
        {
            ResultMessage = "1";
            _dataSource = data;
            _resultFlag = true;
        }

        public JsonResult()
        {

        }

        [DataMember]
        public bool ResultFlag
        {
            get { return _resultFlag; }
            set { _resultFlag = value; }
        }

        [DataMember]
        public string ResultMessage { get; set; }

        [DataMember]
        public object DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; }
        }
    }
}
