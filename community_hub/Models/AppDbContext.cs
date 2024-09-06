using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace community_hub.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Background> Backgrounds { get; set; }

    public virtual DbSet<ChatMessage> ChatMessages { get; set; }

    public virtual DbSet<ChatRoom> ChatRooms { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Community> Communities { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<GroupOwner> GroupOwners { get; set; }

    public virtual DbSet<Invitation> Invitations { get; set; }

    public virtual DbSet<Mention> Mentions { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Policy> Policies { get; set; }

    public virtual DbSet<Poll> Polls { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<React> Reacts { get; set; }

    public virtual DbSet<Reply> Replies { get; set; }

    public virtual DbSet<Resource> Resources { get; set; }

    public virtual DbSet<SavedPost> SavedPosts { get; set; }

    public virtual DbSet<Share> Shares { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAccessLog> UserAccessLogs { get; set; }

    public virtual DbSet<UserChatRoom> UserChatRooms { get; set; }

    public virtual DbSet<UserGroup> UserGroups { get; set; }

    public virtual DbSet<UserSkill> UserSkills { get; set; }

    public virtual DbSet<VoteOption> VoteOptions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=community_hub;user=root;password=admin", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.36-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Background>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("background");

            entity.HasIndex(e => e.UserId, "FK83qbxnkwm5ikyhrcp7ch03xt6");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BackgroundUrl)
                .HasMaxLength(255)
                .HasColumnName("background_url");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Backgrounds)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK83qbxnkwm5ikyhrcp7ch03xt6");
        });

        modelBuilder.Entity<ChatMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("chat_message");

            entity.HasIndex(e => e.RoomId, "FKfvbc4wvhk51y0qtnjrbminxfu");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .HasColumnName("content")
                .UseCollation("utf8mb4_unicode_ci");
            entity.Property(e => e.Date)
                .HasMaxLength(6)
                .HasColumnName("date");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.Sender)
                .HasMaxLength(255)
                .HasColumnName("sender");
            entity.Property(e => e.VoiceUrl)
                .HasMaxLength(5000)
                .HasColumnName("voice_url");

            entity.HasOne(d => d.Room).WithMany(p => p.ChatMessages)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FKfvbc4wvhk51y0qtnjrbminxfu");
        });

        modelBuilder.Entity<ChatRoom>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("chat_room");

            entity.HasIndex(e => e.CommunityId, "FK8fd9runq67rw0mgyq0o38euoo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommunityId).HasColumnName("community_id");
            entity.Property(e => e.Date)
                .HasMaxLength(6)
                .HasColumnName("date");
            entity.Property(e => e.IsDeleted)
                .HasColumnType("bit(1)")
                .HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Photo)
                .HasMaxLength(255)
                .HasColumnName("photo");

            entity.HasOne(d => d.Community).WithMany(p => p.ChatRooms)
                .HasForeignKey(d => d.CommunityId)
                .HasConstraintName("FK8fd9runq67rw0mgyq0o38euoo");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("comment");

            entity.HasIndex(e => e.UserId, "FK8kcum44fvpupyw6f5baccx25c");

            entity.HasIndex(e => e.PostId, "FKs1slvnkuemjsq2kj4h3vhx7i1");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .HasColumnName("content");
            entity.Property(e => e.LocalDateTime)
                .HasMaxLength(6)
                .HasColumnName("local_date_time");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FKs1slvnkuemjsq2kj4h3vhx7i1");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK8kcum44fvpupyw6f5baccx25c");
        });

        modelBuilder.Entity<Community>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("community");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .HasMaxLength(6)
                .HasColumnName("date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.GroupAccess)
                .HasColumnType("enum('PRIVATE','PUBLIC')")
                .HasColumnName("group_access");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("image");
            entity.Property(e => e.IsActive)
                .HasColumnType("bit(1)")
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.OwnerName)
                .HasMaxLength(255)
                .HasColumnName("owner_name");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("event");

            entity.HasIndex(e => e.UserGroupId, "FK24qci0fug5e5csnn7faeppa4s");

            entity.HasIndex(e => e.UserId, "FKi8bsvlthqr8lngsyshiqsodak");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Access).HasColumnName("access");
            entity.Property(e => e.CreatedDate)
                .HasMaxLength(6)
                .HasColumnName("created_date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.EndDate)
                .HasMaxLength(6)
                .HasColumnName("end_date");
            entity.Property(e => e.EventType)
                .HasColumnType("enum('EVENT','VOTE')")
                .HasColumnName("event_type");
            entity.Property(e => e.IsDeleted)
                .HasColumnType("bit(1)")
                .HasColumnName("is_deleted");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("location");
            entity.Property(e => e.Photo)
                .HasMaxLength(255)
                .HasColumnName("photo");
            entity.Property(e => e.StartDate)
                .HasMaxLength(6)
                .HasColumnName("start_date");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UserGroupId).HasColumnName("user_group_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.UserGroup).WithMany(p => p.Events)
                .HasForeignKey(d => d.UserGroupId)
                .HasConstraintName("FK24qci0fug5e5csnn7faeppa4s");

            entity.HasOne(d => d.User).WithMany(p => p.Events)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FKi8bsvlthqr8lngsyshiqsodak");
        });

        modelBuilder.Entity<GroupOwner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("group_owner");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .HasMaxLength(6)
                .HasColumnName("date");
        });

        modelBuilder.Entity<Invitation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("invitation");

            entity.HasIndex(e => e.CommunityId, "FK1p5erdumkmrflrcm970bsbw5m");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommunityId).HasColumnName("community_id");
            entity.Property(e => e.Date)
                .HasMaxLength(6)
                .HasColumnName("date");
            entity.Property(e => e.IsAccepted)
                .HasColumnType("bit(1)")
                .HasColumnName("is_accepted");
            entity.Property(e => e.IsInvited)
                .HasColumnType("bit(1)")
                .HasColumnName("is_invited");
            entity.Property(e => e.IsRemoved)
                .HasColumnType("bit(1)")
                .HasColumnName("is_removed");
            entity.Property(e => e.IsRequested)
                .HasColumnType("bit(1)")
                .HasColumnName("is_requested");
            entity.Property(e => e.RecipientId).HasColumnName("recipient_id");
            entity.Property(e => e.SenderId).HasColumnName("sender_id");

            entity.HasOne(d => d.Community).WithMany(p => p.Invitations)
                .HasForeignKey(d => d.CommunityId)
                .HasConstraintName("FK1p5erdumkmrflrcm970bsbw5m");
        });

        modelBuilder.Entity<Mention>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("mention");

            entity.HasIndex(e => e.PostId, "FKjhk4i11uygeqbvdjd6gwymxyy");

            entity.HasIndex(e => e.CommentId, "FKk27w46whmtdlft4hhf03ybo36");

            entity.HasIndex(e => e.UserId, "FKn31wbwwlg6vkjduifveavloo4");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.Date)
                .HasMaxLength(6)
                .HasColumnName("date");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.PostedUserId).HasColumnName("posted_user_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Comment).WithMany(p => p.Mentions)
                .HasForeignKey(d => d.CommentId)
                .HasConstraintName("FKk27w46whmtdlft4hhf03ybo36");

            entity.HasOne(d => d.Post).WithMany(p => p.Mentions)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FKjhk4i11uygeqbvdjd6gwymxyy");

            entity.HasOne(d => d.User).WithMany(p => p.Mentions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FKn31wbwwlg6vkjduifveavloo4");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("notification");

            entity.HasIndex(e => e.ShareId, "FK9r6e67by12suruth6v9e6uhds");

            entity.HasIndex(e => e.MentionId, "FK9x4jv3je5opby675dbv1htjhn");

            entity.HasIndex(e => e.UserId, "FKb0yvoep4h4k92ipon31wmdf7e");

            entity.HasIndex(e => e.ReplyId, "FKcs557rmx1i8mww5jsodpeobhv");

            entity.HasIndex(e => e.ReactId, "FKeam6sbb6obm0r6x5hkay92lne");

            entity.HasIndex(e => e.CommentId, "FKgmcypgrcb3oo4ujbbk7cyaro2");

            entity.HasIndex(e => e.PostId, "FKn1l10g2mvj4r1qs93k952fshe");

            entity.HasIndex(e => e.EventId, "FKsht3fif7btn0phoy13gvsae3m");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .HasColumnName("content");
            entity.Property(e => e.Date)
                .HasMaxLength(6)
                .HasColumnName("date");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.MentionId).HasColumnName("mention_id");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.ReactId).HasColumnName("react_id");
            entity.Property(e => e.ReplyId).HasColumnName("reply_id");
            entity.Property(e => e.ShareId).HasColumnName("share_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Comment).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.CommentId)
                .HasConstraintName("FKgmcypgrcb3oo4ujbbk7cyaro2");

            entity.HasOne(d => d.Event).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FKsht3fif7btn0phoy13gvsae3m");

            entity.HasOne(d => d.Mention).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.MentionId)
                .HasConstraintName("FK9x4jv3je5opby675dbv1htjhn");

            entity.HasOne(d => d.Post).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FKn1l10g2mvj4r1qs93k952fshe");

            entity.HasOne(d => d.React).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.ReactId)
                .HasConstraintName("FKeam6sbb6obm0r6x5hkay92lne");

            entity.HasOne(d => d.Reply).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.ReplyId)
                .HasConstraintName("FKcs557rmx1i8mww5jsodpeobhv");

            entity.HasOne(d => d.Share).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.ShareId)
                .HasConstraintName("FK9r6e67by12suruth6v9e6uhds");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FKb0yvoep4h4k92ipon31wmdf7e");
        });

        modelBuilder.Entity<Policy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("policy");

            entity.HasIndex(e => e.UserId, "FK5csobu0otstwh6q469a0hw0j0");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .HasMaxLength(6)
                .HasColumnName("date");
            entity.Property(e => e.Rule)
                .HasMaxLength(255)
                .HasColumnName("rule");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Policies)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK5csobu0otstwh6q469a0hw0j0");
        });

        modelBuilder.Entity<Poll>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("poll");

            entity.HasIndex(e => e.VoteOptionId, "FK2uunpuskwftskt4t4cwgeuhdb");

            entity.HasIndex(e => e.UserId, "FK4jhwt6yb2clrgjr9fulktquum");

            entity.HasIndex(e => e.EventId, "FKrujltih9gwf5j8uhv95h52fsg");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .HasMaxLength(6)
                .HasColumnName("date");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.Type)
                .HasColumnType("enum('YES','NO')")
                .HasColumnName("type");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.VoteOptionId).HasColumnName("vote_option_id");

            entity.HasOne(d => d.Event).WithMany(p => p.Polls)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FKrujltih9gwf5j8uhv95h52fsg");

            entity.HasOne(d => d.User).WithMany(p => p.Polls)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK4jhwt6yb2clrgjr9fulktquum");

            entity.HasOne(d => d.VoteOption).WithMany(p => p.Polls)
                .HasForeignKey(d => d.VoteOptionId)
                .HasConstraintName("FK2uunpuskwftskt4t4cwgeuhdb");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("post");

            entity.HasIndex(e => e.UserId, "FK72mt33dhhs48hf9gcqrq4fxte");

            entity.HasIndex(e => e.UserGroupId, "FKkwlgd5n4lopeu8a7oqij490b1");

            entity.HasIndex(e => e.Url, "UK_ppmqe9x5n4ha6pud801evcr8s").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Access)
                .HasColumnType("enum('PUBLIC','PRIVATE')")
                .HasColumnName("access");
            entity.Property(e => e.CreatedDate)
                .HasMaxLength(6)
                .HasColumnName("created_date");
            entity.Property(e => e.Description)
                .HasMaxLength(6000)
                .HasColumnName("description");
            entity.Property(e => e.Hide)
                .HasColumnType("bit(1)")
                .HasColumnName("hide");
            entity.Property(e => e.IsDeleted)
                .HasColumnType("bit(1)")
                .HasColumnName("is_deleted");
            entity.Property(e => e.PostType)
                .HasColumnType("enum('CONTENT','RESOURCE','RAW')")
                .HasColumnName("post_type");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.UserGroupId).HasColumnName("user_group_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.UserGroup).WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserGroupId)
                .HasConstraintName("FKkwlgd5n4lopeu8a7oqij490b1");

            entity.HasOne(d => d.User).WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK72mt33dhhs48hf9gcqrq4fxte");
        });

        modelBuilder.Entity<React>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("react");

            entity.HasIndex(e => e.UserId, "FK2lflwxjly6yyekdm33tggpt2k");

            entity.HasIndex(e => e.CommentId, "FKhqmplk2gqo9y4pnih80b1g3om");

            entity.HasIndex(e => e.EventId, "FKjrttoc06ctxewt1xobjq03sj8");

            entity.HasIndex(e => e.ReplyId, "FKpsuwl0uin291ltihc4dj88s9t");

            entity.HasIndex(e => e.PostId, "FKsx6x7cumu9xl1gdv1uorfb5qa");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.Date)
                .HasMaxLength(6)
                .HasColumnName("date");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.ReplyId).HasColumnName("reply_id");
            entity.Property(e => e.Type)
                .HasColumnType("enum('LIKE','HAHA','LOVE','ANGRY','SAD','CARE','WOW','DISLIKE','OTHER')")
                .HasColumnName("type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Comment).WithMany(p => p.Reacts)
                .HasForeignKey(d => d.CommentId)
                .HasConstraintName("FKhqmplk2gqo9y4pnih80b1g3om");

            entity.HasOne(d => d.Event).WithMany(p => p.Reacts)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FKjrttoc06ctxewt1xobjq03sj8");

            entity.HasOne(d => d.Post).WithMany(p => p.Reacts)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FKsx6x7cumu9xl1gdv1uorfb5qa");

            entity.HasOne(d => d.Reply).WithMany(p => p.Reacts)
                .HasForeignKey(d => d.ReplyId)
                .HasConstraintName("FKpsuwl0uin291ltihc4dj88s9t");

            entity.HasOne(d => d.User).WithMany(p => p.Reacts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK2lflwxjly6yyekdm33tggpt2k");
        });

        modelBuilder.Entity<Reply>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("reply");

            entity.HasIndex(e => e.CommentId, "FK6w0ns67lrq1jdiwi5xvtj1vxx");

            entity.HasIndex(e => e.UserId, "FKapyyxlgntertu5okpkr685ir9");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .HasColumnName("content");
            entity.Property(e => e.LocalDateTime)
                .HasMaxLength(6)
                .HasColumnName("local_date_time");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Comment).WithMany(p => p.Replies)
                .HasForeignKey(d => d.CommentId)
                .HasConstraintName("FK6w0ns67lrq1jdiwi5xvtj1vxx");

            entity.HasOne(d => d.User).WithMany(p => p.Replies)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FKapyyxlgntertu5okpkr685ir9");
        });

        modelBuilder.Entity<Resource>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("resource");

            entity.HasIndex(e => e.PostId, "FK8ilxm0n6p92sgjtuamfbyfj57");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .HasMaxLength(6)
                .HasColumnName("date");
            entity.Property(e => e.Description)
                .HasMaxLength(6000)
                .HasColumnName("description");
            entity.Property(e => e.Photo)
                .HasMaxLength(255)
                .HasColumnName("photo");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.Raw)
                .HasMaxLength(255)
                .HasColumnName("raw");
            entity.Property(e => e.Video)
                .HasMaxLength(255)
                .HasColumnName("video");

            entity.HasOne(d => d.Post).WithMany(p => p.Resources)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK8ilxm0n6p92sgjtuamfbyfj57");
        });

        modelBuilder.Entity<SavedPost>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("saved_post");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.SavedDate)
                .HasMaxLength(6)
                .HasColumnName("saved_date");
            entity.Property(e => e.SaverId).HasColumnName("saver_id");
        });

        modelBuilder.Entity<Share>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("share");

            entity.HasIndex(e => e.UserId, "FKhwfan84wqm1mflkf4dp8wcic0");

            entity.HasIndex(e => e.PostId, "FKjvqve4c7t902i00bw4q6bm36r");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Caption)
                .HasMaxLength(255)
                .HasColumnName("caption");
            entity.Property(e => e.Date)
                .HasMaxLength(6)
                .HasColumnName("date");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Post).WithMany(p => p.Shares)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FKjvqve4c7t902i00bw4q6bm36r");

            entity.HasOne(d => d.User).WithMany(p => p.Shares)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FKhwfan84wqm1mflkf4dp8wcic0");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("skill");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.BannedReason)
                .HasMaxLength(255)
                .HasColumnName("banned_reason");
            entity.Property(e => e.Dept)
                .HasMaxLength(255)
                .HasColumnName("dept");
            entity.Property(e => e.Division)
                .HasMaxLength(255)
                .HasColumnName("division");
            entity.Property(e => e.Dob)
                .HasMaxLength(255)
                .HasColumnName("dob");
            entity.Property(e => e.Done)
                .HasColumnType("bit(1)")
                .HasColumnName("done");
            entity.Property(e => e.DoorLogNum).HasColumnName("door_log_num");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FullyPermitted)
                .HasColumnType("bit(1)")
                .HasColumnName("fully_permitted");
            entity.Property(e => e.Gender)
                .HasMaxLength(255)
                .HasColumnName("gender");
            entity.Property(e => e.Hobby)
                .HasMaxLength(255)
                .HasColumnName("hobby");
            entity.Property(e => e.IsActive)
                .HasColumnType("bit(1)")
                .HasColumnName("is_active");
            entity.Property(e => e.IsDeleted)
                .HasColumnType("bit(1)")
                .HasColumnName("is_deleted");
            entity.Property(e => e.IsOn)
                .HasColumnType("enum('ON','OFF')")
                .HasColumnName("is_on");
            entity.Property(e => e.IsRejected)
                .HasColumnType("bit(1)")
                .HasColumnName("is_rejected");
            entity.Property(e => e.IsRemoved)
                .HasColumnType("bit(1)")
                .HasColumnName("is_removed");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Pending)
                .HasColumnType("bit(1)")
                .HasColumnName("pending");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasColumnName("phone");
            entity.Property(e => e.Photo)
                .HasMaxLength(255)
                .HasColumnName("photo");
            entity.Property(e => e.RejectReason)
                .HasMaxLength(255)
                .HasColumnName("reject_reason");
            entity.Property(e => e.RejectedCount).HasColumnName("rejected_count");
            entity.Property(e => e.RemovedReason)
                .HasMaxLength(255)
                .HasColumnName("removed_reason");
            entity.Property(e => e.Role)
                .HasColumnType("enum('ADMIN','USER','DEFAULT_USER')")
                .HasColumnName("role");
            entity.Property(e => e.StaffId)
                .HasMaxLength(255)
                .HasColumnName("staff_id");
            entity.Property(e => e.Team)
                .HasMaxLength(255)
                .HasColumnName("team");
        });

        modelBuilder.Entity<UserAccessLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user_access_log");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccessTime)
                .HasMaxLength(6)
                .HasColumnName("access_time");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.ErrorMessage)
                .HasMaxLength(255)
                .HasColumnName("error_message");
            entity.Property(e => e.Type)
                .HasColumnType("enum('SIGN_IN','SIGN_OUT','ERROR')")
                .HasColumnName("type");
        });

        modelBuilder.Entity<UserChatRoom>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user_chat_room");

            entity.HasIndex(e => e.RoomId, "FKkl7p10k4h9yj2rnyild4gs93m");

            entity.HasIndex(e => e.UserId, "FKrglxi17nh7wv7fsd5sp7nrc0f");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .HasMaxLength(6)
                .HasColumnName("date");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Room).WithMany(p => p.UserChatRooms)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FKkl7p10k4h9yj2rnyild4gs93m");

            entity.HasOne(d => d.User).WithMany(p => p.UserChatRooms)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FKrglxi17nh7wv7fsd5sp7nrc0f");
        });

        modelBuilder.Entity<UserGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user_group");

            entity.HasIndex(e => e.UserId, "FK1c1dsw3q36679vaiqwvtv36a6");

            entity.HasIndex(e => e.CommunityId, "FK3pt3kosqqsx1wyx771lu36g5y");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommunityId).HasColumnName("community_id");
            entity.Property(e => e.Date)
                .HasMaxLength(6)
                .HasColumnName("date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Community).WithMany(p => p.UserGroups)
                .HasForeignKey(d => d.CommunityId)
                .HasConstraintName("FK3pt3kosqqsx1wyx771lu36g5y");

            entity.HasOne(d => d.User).WithMany(p => p.UserGroups)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK1c1dsw3q36679vaiqwvtv36a6");
        });

        modelBuilder.Entity<UserSkill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user_skill");

            entity.HasIndex(e => e.UserId, "FKfixgsonf2ev168mfck7co17u1");

            entity.HasIndex(e => e.SkillId, "FKj53flyds4vknyh8llw5d7jdop");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Experience)
                .HasMaxLength(255)
                .HasColumnName("experience");
            entity.Property(e => e.SkillId).HasColumnName("skill_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Skill).WithMany(p => p.UserSkills)
                .HasForeignKey(d => d.SkillId)
                .HasConstraintName("FKj53flyds4vknyh8llw5d7jdop");

            entity.HasOne(d => d.User).WithMany(p => p.UserSkills)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FKfixgsonf2ev168mfck7co17u1");
        });

        modelBuilder.Entity<VoteOption>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("vote_option");

            entity.HasIndex(e => e.EventId, "FKuh4fb0o4weg0gla4qxj5lslw");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.IsDeleted)
                .HasColumnType("bit(1)")
                .HasColumnName("is_deleted");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");

            entity.HasOne(d => d.Event).WithMany(p => p.VoteOptions)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FKuh4fb0o4weg0gla4qxj5lslw");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
