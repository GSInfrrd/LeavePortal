using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeLeaveManagementApp.Models
{
    public class ProjectDetails
    {
        public string ProjectName { get; set; }
        public string Description { get; set; }

        public List<string> Technologies { get; set; }
        public List<string> TechnologyDetails { get; set; }
        public DateTime StartDate { get; set; }
        public int RefManager { get; set; }
    }

    public class TechnologyDetails
    {
        public int Id { get; set; }
        public string Technology { get; set; }
        public string RefEmployeeId { get; set; }
        public bool IsSelected { get; set; }
    }

    public class TechnologyDescriptions
    {
        public int Id { get; set; }
        public int RefTechnology { get; set; }
        public string TechnologyDetails { get; set; }
        public string RefEmployeeId { get; set; }
        public bool IsSelected { get; set; }
    }
}