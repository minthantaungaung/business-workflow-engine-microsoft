using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qal.workflow.domain.Data.DTO.RuleDTOs
{
    public class Assignee
    {
        public int Cases { get; set; }
        public string? EmployeeId { get; set; }
        public string? Name { get; set; }
    }

    public class OperatorAssignDto : Property
    {
        public string? DocumentNo { get; set; }
        public List<Assignee>? Assignees { get; set; }
    }

    public class AssigneeResponse
    {
        public string? DocumentNo { get; set; }
        public int Cases { get; set; }
        public string? EmployeeId { get; set; }
        public string? Name { get; set; }
    }
}
