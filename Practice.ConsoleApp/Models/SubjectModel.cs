using Ans.Net10.Common;
using System.Xml;
using System.Xml.Serialization;

namespace Practice.ConsoleApp.Models
{
    public class SubjectModel
    {

        /* serialization properties */

        [XmlAttribute("IDSubg")]
        public string IDSubg { get; set; }

        [XmlAttribute("disc")]
        public string Disc { get; set; }

        [XmlAttribute("chair")]
        public string Chair { get; set; }

        [XmlAttribute("id_prep")]
        public string Id_prepRaw { get; set; }

        [XmlAttribute("prep")]
        public string PrepRaw { get; set; }

        [XmlAttribute("id_group")]
        public string Id_groupRaw { get; set; }

        [XmlAttribute("group")]
        public string GroupRaw { get; set; }

        [XmlAttribute("day")]
        public string DayRaw { get; set; }

        [XmlAttribute("less")]
        public string Less { get; set; }

        [XmlAttribute("buildings")]
        public string Buildings { get; set; }

        [XmlAttribute("rooms")]
        public string Rooms { get; set; }

        /* readonly properties */

        [XmlIgnore]
        public DateOnly? Day
        {
            get => field ??= DayRaw.ToDateOnly();
        }

        [XmlIgnore]
        public int[] PrepIds
        {
            get => field ??= Id_prepRaw.ToIntArray();
        }
        [XmlIgnore]
        public int[] GroupIds
        {
            get => field ??= Id_groupRaw.ToIntArray();
        }
        [XmlIgnore]
        public string[] PrepNames
        {
            get
            {
                if (string.IsNullOrWhiteSpace(PrepRaw))
                    return Array.Empty<string>();

                return PrepRaw
                    .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(item => item.Trim())
                    .ToArray();
            }
        }

        [XmlIgnore]
        public string[] GroupNames
        {
            get
            {
                if (string.IsNullOrWhiteSpace(GroupRaw))
                    return Array.Empty<string>();

                return GroupRaw
                    .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(item => item.Trim())
                    .ToArray();
            }
        }
    }


}






