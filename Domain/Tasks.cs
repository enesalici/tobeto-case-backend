using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; } 
        public TaskStatus Status { get; set; } 
        public int UserId { get; set; }
        public User User { get; set; }
    }
}

public enum TaskStatus
{
    New,
    InProgress,
    Completed
}