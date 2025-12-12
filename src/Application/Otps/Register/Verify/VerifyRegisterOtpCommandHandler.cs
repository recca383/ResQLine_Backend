using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Otps.Register.Verify;
internal sealed class VerifyRegisterOtpCommandHandler(
    //IApplicationDbContext context
    ) :
    ICommandHandler<VerifyRegisterOtpCommand>
{
    public Task<Result> Handle(VerifyRegisterOtpCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

