using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Poco;
using EF.Mapping;

namespace EF
{
    public class Context : BaseContext
    {
        public DbSet<Account> Account { get; set; }
        public DbSet<AccountMain> AccountMain { get; set; }
        public DbSet<ActivateEmail> ActivateEmail { get; set; }
        public DbSet<AutoMessage_Add> AutoMessage_Add { get; set; }
        public DbSet<AutoMessage_Keyword> AutoMessage_Keyword { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<LibraryImage> LibraryImage { get; set; }
        public DbSet<LibraryImageText> LibraryImageText { get; set; }
        public DbSet<LibraryText> LibraryText { get; set; }
        public DbSet<LibraryVideo> LibraryVideo { get; set; }
        public DbSet<LibraryVoice> LibraryVoice { get; set; }
        public DbSet<Lookup> Lookup { get; set; }
        public DbSet<LookupOption> LookupOption { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<SystemUser> SystemUser { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Account_AccountMain> Account_AccountMain { get; set; }
        public DbSet<ClientInfo> ClientInfo { get; set; }
        public DbSet<Keyword> Keyword { get; set; }
        public DbSet<Conversation> Conversation { get; set; }
        public DbSet<OrderMType> OrderMType { get; set; }
        public DbSet<SurveyAnswerUser> SurveyAnswerUser { get; set; }
        public DbSet<Property_User> Property_User { get; set; }
        public DbSet<Recipes> Recipes { get; set; }
        public DbSet<LifeSkill> LifeSkill { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Ignore<PropertyComplexEntity>();
            modelBuilder.Configurations.Add(new LibraryImageTextMap());
            modelBuilder.Configurations.Add(new SystemUserMap());
            modelBuilder.Configurations.Add(new ClientInfoMap());
            modelBuilder.Configurations.Add(new MessageMap());
            modelBuilder.Configurations.Add(new AccountMainMap());
            modelBuilder.Configurations.Add(new AccountMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserLoginInfoMap());
            modelBuilder.Configurations.Add(new AutoMessage_KeywordMap());
            modelBuilder.Configurations.Add(new KeywordAutoMessageMap());
            modelBuilder.Configurations.Add(new KeywordMap());
            modelBuilder.Configurations.Add(new AppUpdateMap());
            modelBuilder.Configurations.Add(new PendingMessagesMap());
            modelBuilder.Configurations.Add(new PushMsgMap());
            modelBuilder.Configurations.Add(new TaskMap());
            modelBuilder.Configurations.Add(new RoleMenuMap());
            modelBuilder.Configurations.Add(new Lottery_dish_detailMap());
        }
    }
}
