using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Poco;

namespace EF.Mapping
{
    public class TaskMap : EntityTypeConfiguration<Task>
    {
        public TaskMap()
        {
            this.HasRequired(a => a.CreateAccount)
            .WithMany(a => a.Tasks_Creater)
            .HasForeignKey(a => a.CreateAccountID);

            this.HasRequired(a => a.ReceiverAccount)
            .WithMany(a => a.Tasks_Receiver)
            .HasForeignKey(a => a.ReceiverAccountID);
        }
    }
}
