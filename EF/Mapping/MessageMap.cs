using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Poco;

namespace EF.Mapping
{
    public class MessageMap : EntityTypeConfiguration<Message>
    {
        public MessageMap()
        {
            this.HasOptional(a => a.FromAccount)
            .WithMany(a => a.SendMessages)
            .HasForeignKey(a => a.FromAccountID);

            this.HasOptional(a => a.ToAccount)
            .WithMany(a => a.ReceiveMessages)
            .HasForeignKey(a => a.ToAccountID);

            this.HasOptional(a => a.FromUser)
            .WithMany(a => a.SendMessages)
            .HasForeignKey(a => a.FromUserID);

            this.HasOptional(a => a.ToUser)
            .WithMany(a => a.ReceiveMessages)
            .HasForeignKey(a => a.ToUserID);

            this.HasRequired(a => a.EnumMessageSendDirection)
            .WithMany(a => a.MessageEnumMessageSendDirection)
            .HasForeignKey(a => a.EnumMessageSendDirectionID);

            this.HasRequired(a => a.EnumMessageType)
            .WithMany(a => a.MessageEnumMessageType)
            .HasForeignKey(a => a.EnumMessageTypeID);
        }
    }
}
