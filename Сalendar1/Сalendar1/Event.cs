using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Сalendar1
{
    class Event
    {
        public string Title { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime? Reminder { get; set; }
    }
}
