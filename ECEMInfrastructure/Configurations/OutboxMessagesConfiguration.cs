using ECEMInfrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ECEMInfrastructure.Configurationss;
internal sealed class OutboxMessagesConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {  
        builder.Property(outboxMessages => outboxMessages.Content)
            .HasColumnType("nvarchar(max)")
            .IsRequired();
    }
}