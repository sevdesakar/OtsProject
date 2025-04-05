using MediatR;
using Ots.Base;
using Ots.Schema;

namespace Ots.Api.Impl.Cqrs;

public record GetAccountTransactionByIdQuery(int Id) : IRequest<ApiResponse<AccountTransactionResponse>>;
public record GetAccountTransactionByParametersQuery() : IRequest<ApiResponse<List<AccountTransactionResponse>>>;