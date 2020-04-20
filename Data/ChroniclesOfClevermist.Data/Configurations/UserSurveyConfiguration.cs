namespace ChroniclesOfClevermist.Data.Configurations
{
    using ChroniclesOfClevermist.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserSurveyConfiguration : IEntityTypeConfiguration<UserSurvey>
    {
        public void Configure(EntityTypeBuilder<UserSurvey> builder)
        {
            builder.HasKey(cc => new
            {
                cc.UserId,
                cc.SurveyId,
            });
        }
    }
}
