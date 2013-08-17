using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using System.Data.Entity.ModelConfiguration;

namespace EF.Mapping
{
    public class PendingMessagesMap : EntityTypeConfiguration<PendingMessages>
    {
        public PendingMessagesMap()
        {
            this.HasOptional(a => a.FromAccount)
                .WithMany(a => a.SendPendingMessages)
                .HasForeignKey(a => a.FromAccountID);

            this.HasOptional(a => a.ToAccount)
                .WithMany(a => a.ReceivePendingMessages)
                .HasForeignKey(a => a.ToAccountID);

            this.HasOptional(a => a.FromUser)
                .WithMany(a => a.SendPendingMessages)
                .HasForeignKey(a => a.FromUserID);

            this.HasOptional(a => a.ToUser)
                .WithMany(a => a.ReceivePendingMessages)
                .HasForeignKey(a => a.ToUserID);
        }
    }
}
