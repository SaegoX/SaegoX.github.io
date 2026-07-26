using System.Xml;
using System.Xml.Serialization;

namespace Practice.ConsoleApp.Models
{
    [XmlRoot("rasp")]
    public class RaspModel
    {
        [XmlElement("subject")]
        public SubjectModel[] Subject { get; set; }
    }


}






