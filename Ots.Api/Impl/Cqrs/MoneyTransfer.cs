using MediatR;
using Ots.Base;
using Ots.Schema;

namespace Ots.Api.Impl.Cqrs;

public record GetAllMoneyTransfersQuery : IRequest<ApiResponse<List<MoneyTransferResponse>>>;
public record GetMoneyTransferByIdQuery(int Id) : IRequest<ApiResponse<MoneyTransferResponse>>;
public record CreateMoneyTransferCommand(MoneyTransferRequest MoneyTransfer) : IRequest<ApiResponse<MoneyTransferResponse>>;
public record UpdateMoneyTransferCommand(int Id, MoneyTransferRequest MoneyTransfer) : IRequest<ApiResponse>;
public record DeleteMoneyTransferCommand(int Id) : IRequest<ApiResponse>;