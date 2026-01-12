

using ECEMCore.Abstraction.Emailing;

namespace ECEMInfrastructure.Services.Emailling
{
    public class EmailService : IEmailService
    {
        public Task sendAsync()
        {
            return Task.CompletedTask;
        } 

    }
}
