using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Poco;

namespace EF.Mapping
{
    public class AutoMessage_KeywordMap : EntityTypeConfiguration<AutoMessage_Keyword>
    {
        public AutoMessage_KeywordMap()
        {
            this.HasOptional(a => a.ParentAutoMessage_Keyword)
            .WithMany(a => a.AutoMessage_KeywordsKeyword)
            .HasForeignKey(a => a.ParentAutoMessage_KeywordID);
        }
    }
}
