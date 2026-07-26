using System;
using System.Collections.Generic;
using System.Text;

namespace Practice.ConsoleApp.Models
{
    public class ExamModel
    {
        public string Id {  get; set; }
        public DateOnly? Date { get; set; }
        public string Disc {  get; set; }
        public string Preps { get; set; } 
        public string Groups { get; set; }
        public string Less {  get; set; }
        public string Room { get; set; }
        public string Building { get; set; }
    }
}
