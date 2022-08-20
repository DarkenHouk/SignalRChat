using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignalRChat.Models;

namespace SignalRChat.DB
{
    public class UserChatRoomConfiguration : IEntityTypeConfiguration<UserChatRoom>
    {
        public void Configure(EntityTypeBuilder<UserChatRoom> builder)
        {
            builder.HasKey(userChatRoom => new { userChatRoom.UserId, userChatRoom.ChatRoomId });

            builder.HasOne(userChatRoom => userChatRoom.User)
                .WithMany(u => u.UserChatRooms)
                .HasForeignKey(userChatRoom => userChatRoom.UserId);

            builder.HasOne(userChatRoom => userChatRoom.ChatRoom)
                .WithMany(cr => cr.UserChatRooms)
                .HasForeignKey(userChatRoom => userChatRoom.ChatRoomId);

            builder.HasOne(userChatRoom => userChatRoom.LastReadMessage)
                .WithMany(m => m.LastReadByUsers)
                .HasForeignKey(userChatRoom => userChatRoom.LastReadMessageId);
        }
    }
}
