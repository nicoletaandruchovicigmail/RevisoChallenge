using System;
using RevisoChallenge.DAL.Entities;
using RevisoChallenge.DAL.Services.Implementation;

namespace RevisoChallenge.Models
{
    public class InvoiceViewModel
    {
        public InvoiceViewModel() { }

        public InvoiceViewModel(Invoice invoice)
        {
            Id = invoice.Id;
            ProjectId = invoice.ProjectId;
        }
      
        public int Id { get; set; }
        public int ProjectId { get; set; }
    }
}