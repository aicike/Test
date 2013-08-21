using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Poco;

namespace EF.Mapping
{
    public class KeywordAutoMessageMap : EntityTypeConfiguration<KeywordAutoMessage>
    {
        public KeywordAutoMessageMap()
        {
            this.HasRequired(a => a.AutoMessage_Keyword)
            .WithMany(a => a.KeywordAutoMessages)
            .HasForeignKey(a => a.AutoMessage_KeywordID).WillCascadeOnDelete(true);

            this.Ignore(a => a.SendTime);
        }
    }
}
