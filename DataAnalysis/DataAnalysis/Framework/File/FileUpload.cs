using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DataAnalysis.Framework.File
{
    [Serializable]
    [DataContract]
    public class FileUpload
    {
        [DataMember]
        public string SourceFile
        {
            get;
            set;
        }
        [DataMember]
        public string SourceFileName
        {
            get;
            set;
        }
        [DataMember]
        public string TargetFile
        {
            get;
            set;
        }
        [DataMember]
        public string TargetFileName
        {
            get;
            set;
        }

        [DataMember]
        public string UserFileName
        {
            get;
            set;
        }
    }
}