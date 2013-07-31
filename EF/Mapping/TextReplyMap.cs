using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Poco;

namespace EF.Mapping
{
    public class TextReplyMap : EntityTypeConfiguration<TextReply>
    {
        public TextReplyMap()
        {
            this.HasRequired(a => a.AutoMessage_Keyword)
            .WithMany(a => a.TextReplys)
            .HasForeignKey(a => a.AutoMessage_KeywordID).WillCascadeOnDelete(true);
        }
    }
}
