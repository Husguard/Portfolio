using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Models
{
    public class User
    {
        public int Id { get; set; }
        public int GUID { get; set; }
        public ICollection<Message> Messages { get; set; }
        public User()
        {
            Messages = new List<Message>();
        }
    }
}
