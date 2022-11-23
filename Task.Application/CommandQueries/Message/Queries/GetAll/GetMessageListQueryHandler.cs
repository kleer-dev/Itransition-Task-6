using MediatR;
using Task.Application.Common.Interfaces;

namespace Task.Application.CommandQueries.Message.Queries.GetAll;

public class GetMessageListQueryHandler : IRequestHandler<GetMessageListQuery, MessagesListVm>
{
    private readonly IApplicationContext _context;

    public GetMessageListQueryHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task<MessagesListVm> Handle(GetMessageListQuery request,
        CancellationToken cancellationToken)
    {
        var user = _context.Users
            .FirstOrDefault(u => u.Name == request.Name);

        if (user is null)
            return new MessagesListVm
            {
                Messages = new List<Domain.Message>()
            };

        var messages = _context.Messages.Where(m => m.User.Id == user.Id);

        return new MessagesListVm { Messages = messages };
    }
}