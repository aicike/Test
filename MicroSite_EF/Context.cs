using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Poco;
using MicroSite_EF.Mapping;
using EF;

namespace MicroSite_EF
{
    public class Context : BaseContext
    {
        //public DbSet<AccountMain> AccountMain { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Ignore<AppUpdate>();
            modelBuilder.Ignore<LibraryImageText>();
            modelBuilder.Ignore<SystemUser>();
            modelBuilder.Ignore<Message>();
            modelBuilder.Ignore<AutoMessage_Keyword>();
            modelBuilder.Ignore<KeywordAutoMessage>();
            modelBuilder.Ignore<Keyword>();
            modelBuilder.Ignore<AppUpdate>();
            modelBuilder.Ignore<PendingMessages>();
            modelBuilder.Ignore<PushMsg>();
            modelBuilder.Ignore<Task>();
            modelBuilder.Ignore<PropertyComplexEntity>();

            //modelBuilder.Configurations.Add(new LibraryImageTextMap());
            //modelBuilder.Configurations.Add(new SystemUserMap());
            modelBuilder.Configurations.Add(new ClientInfoMap());
            //modelBuilder.Configurations.Add(new MessageMap());
            modelBuilder.Configurations.Add(new AccountMainMap());
            modelBuilder.Configurations.Add(new AccountMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserLoginInfoMap());
            //modelBuilder.Configurations.Add(new AutoMessage_KeywordMap());
            //modelBuilder.Configurations.Add(new KeywordAutoMessageMap());
            //modelBuilder.Configurations.Add(new KeywordMap());
            //modelBuilder.Configurations.Add(new AppUpdateMap());
            //modelBuilder.Configurations.Add(new PendingMessagesMap());
            //modelBuilder.Configurations.Add(new PushMsgMap());
            //modelBuilder.Configurations.Add(new TaskMap());
        }
    }
}
