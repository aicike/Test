using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Poco;

namespace EF.Mapping
{
    public class LibraryImageTextMap : EntityTypeConfiguration<LibraryImageText>
    {
        public LibraryImageTextMap()
        {
            this.HasOptional(a => a.LibraryImageTextParent)
            .WithMany(a => a.LibraryImageTexts)
            .HasForeignKey(a => a.LibraryImageTextParentID);
        }
    }
}
