using qal.workflow.domain.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qal.waiter.domain.Data.DTO.Document
{
    public class Document : Property
    {
        public string? DocumentID { get; set; }
        public string? DocumentType { get; set; }
        public string? DocumentStatus { get; set; }
        public string? GPS { get; set; }
        public int GPSNo { get; set; }
    }

    public class ProcessDocumentRequestDto : Property
    {
        public string? ActionUserRole { get; set; }
        public string? ActionUserDecisionType { get; set; }
        public List<List<string>>? ListToCheck { get; set; }
    }

    public class ProcessDocumentResponseDto : Property
    {
        public bool IsSuccess { get; set; }
        public string? Receiver { get; set; }
        public string? Message { get; set; }
        public List<ProcessedDocument>? ProcessedDocuments { get; set; } = new List<ProcessedDocument>();
    }
    public class ProcessedDocument : Property
    {
        public string? DocumentID { get; set; }
        public string? DocumentType { get; set; }
        public string? DocumentStatus { get; set; }
        public string? GPS { get; set; }
        public int GPSNo { get; set; }
        public string? Message { get; set; }
        public string? NextAction { get; set; }
    }
}
