﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ebiz.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EbizEntitiesEntities1 : DbContext
    {
        public EbizEntitiesEntities1()
            : base("name=EbizEntitiesEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BusinessCatMst> BusinessCatMsts { get; set; }
        public virtual DbSet<CommunicationMst> CommunicationMsts { get; set; }
        public virtual DbSet<ComplainMaster> ComplainMasters { get; set; }
        public virtual DbSet<FeedbackMaster> FeedbackMasters { get; set; }
        public virtual DbSet<GrpMemberMst> GrpMemberMsts { get; set; }
        public virtual DbSet<GrpMst> GrpMsts { get; set; }
        public virtual DbSet<GuestMaster> GuestMasters { get; set; }
        public virtual DbSet<MeetingMemberMst> MeetingMemberMsts { get; set; }
        public virtual DbSet<MeetingMst> MeetingMsts { get; set; }
        public virtual DbSet<ReferralMst> ReferralMsts { get; set; }
        public virtual DbSet<RequestForAddMember> RequestForAddMembers { get; set; }
        public virtual DbSet<UserMaster> UserMasters { get; set; }
        public virtual DbSet<VoteMst> VoteMsts { get; set; }
        public virtual DbSet<CandidateMst> CandidateMsts { get; set; }
        public virtual DbSet<VotingMst> VotingMsts { get; set; }
    }
}