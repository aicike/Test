using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using ShortURL.Models;

namespace ShortURL.EF.Mapping
{
    public class URLMapMap : EntityTypeConfiguration<URLMap>
    {
        public URLMapMap()
        {
            this.Property(a=>a.ShortUrl).IsUnicode(true);
        }
    }
}