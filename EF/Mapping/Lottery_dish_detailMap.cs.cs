﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Poco;

namespace EF.Mapping
{
    public class Lottery_dish_detailMap : EntityTypeConfiguration<Lottery_dish_detail>
    {
        public Lottery_dish_detailMap()
        {
            this.Ignore(a => a.IsNewImg);

        }
    }
}